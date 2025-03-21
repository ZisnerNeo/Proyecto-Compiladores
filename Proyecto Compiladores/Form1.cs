using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Proyecto_Compiladores
{
    public partial class Form1 : Form
    {


        private readonly Dictionary<char, int> operador;
        private readonly Dictionary<string, int> palabraReservada;
        private readonly Dictionary<string, int> simbolosEspeciales;
        private List<char> alfabeto = new List<char>();
        private AFN AFD;
        private List<string> NoTerminales = new List<string>();
        private bool errorlexico = false;
        public Form1()
        {
            InitializeComponent();
            palabraReservada = new Dictionary<string, int>
        {
            { "if", 1},
            { "then", 2},
            { "else", 3},
            { "end", 4},
            { "repeat", 5},
            { "until", 6},
            { "read", 7},
            { "write", 8},
        };

            simbolosEspeciales = new Dictionary<string, int>
        {
            { "+", 1},
            { "-", 2},
            { "*", 3},
            { "/", 4},
            { "=", 5},
            { "<", 6},
            { ">", 7},
            { "(", 8},
            { ")", 9},
            { ";", 10},
            { ":=", 11},
        };

            operador = new Dictionary<char, int>
        {
            { '*', 3 },
            { '+', 3 },
            { '?', 3 },
            { '&', 2 },
            { '|', 1 },
            { '(', 4 },
            { ')', 4 },

        };
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string infixRegex = txtInput.Text;
            try
            {
                string expandedRegex = ExpandCharacterRanges(infixRegex);
                string postfixRegex = ConvertToPostfix(AddExplicitConcatenation(expandedRegex));
                txtOutput.Text = postfixRegex;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExpandCharacterRanges(string regex)
        {
            // Expansión de rangos como [a-z], [A-Z], [0-9], [1-6], etc.
            return Regex.Replace(regex, @"\[([^\]]+)\]", match =>
            {
                string content = match.Groups[1].Value;
                StringBuilder expanded = new StringBuilder();

                // Manejar rangos como 1-6, a-z, A-Z, etc.
                for (int i = 0; i < content.Length; i++)
                {
                    if (i + 2 < content.Length && content[i + 1] == '-')
                    {
                        char start = content[i];
                        char end = content[i + 2];
                        for (char c = start; c <= end; c++)
                        {
                            expanded.Append(c);
                            if (c < end) expanded.Append("|");
                        }
                        i += 2; // Saltar el rango (por ejemplo, "1-6")
                    }
                    else
                    {
                        expanded.Append(content[i]);
                        if (i < content.Length - 1) expanded.Append("|");
                    }
                }
                return "(" + expanded.ToString() + ")";
            });
        }

        private string AddExplicitConcatenation(string regex)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < regex.Length; i++)
            {
                char c = regex[i];
                output.Append(c);

                if (i < regex.Length - 1)
                {
                    char next = regex[i + 1];

                    // Condiciones para agregar concatenación explícita (&)
                    if ((char.IsLetterOrDigit(c) || c == ')' || c == '*' || c == '?' || c == '+') &&
                        (char.IsLetterOrDigit(next) || next == '(' || next == '['))
                    {
                        output.Append("&");
                    }
                }
            }
            return output.ToString();
        }

        private string ConvertToPostfix(string infix)
        {
            Dictionary<char, int> precedence = new Dictionary<char, int>
            {
                {'|', 1},  // Unión
                {'&', 2},  // Concatenación explícita
                {'*', 3},  // Cerradura de Kleene
                {'?', 3},  // Opcional
                {'+', 3}   // Cerradura positiva
            };

            Stack<char> operators = new Stack<char>();
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];

                if (char.IsLetterOrDigit(c))
                {
                    output.Append(c);
                }
                else if (c == '(')
                {
                    operators.Push(c);
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        output.Append(operators.Pop());
                    }
                    operators.Pop(); // Eliminar '(' de la pila
                }
                else if (precedence.ContainsKey(c))
                {
                    while (operators.Count > 0 && precedence.ContainsKey(operators.Peek()) && precedence[operators.Peek()] >= precedence[c])
                    {
                        output.Append(operators.Pop());
                    }
                    operators.Push(c);
                }
            }

            // Vaciar la pila de operadores restantes
            while (operators.Count > 0)
            {
                output.Append(operators.Pop());
            }

            return output.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //Boton para AFN
        {
            alfabeto.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            AFN automata = ConvertToAFN(txtOutput.Text);
            ContarEstadosYtransi(automata);
            creartabla(automata, alfabeto);
            AFD = automata;
        }

        private AFN ConvertToAFN(string Posfija)
        {
            if (Posfija == "")
            {
                return new AFN();
            }
            Stack<AFN> stackAutomatas = new Stack<AFN>();

            AFN automata, autAux;
            for (int i = 0; i < Posfija.Length; i++)
            {
                char token = Posfija[i];


                if (char.IsLetterOrDigit(token))
                {
                    automata = new AFN(token);
                    stackAutomatas.Push(automata);
                    alfabeto.Add(token);
                }
                else if (operador.ContainsKey(token))
                {

                    switch (token)
                    {
                        case '+':
                            automata = AFN.CerraduraPositiva(stackAutomatas.Pop());
                            stackAutomatas.Push(automata);
                            break;
                        case '?':
                            automata = AFN.CeroOinstancia(stackAutomatas.Pop());
                            stackAutomatas.Push(automata);
                            break;
                        case '*':
                            automata = new AFN(stackAutomatas.Pop());
                            stackAutomatas.Push(automata);
                            break;
                        case '&':
                            autAux = stackAutomatas.Pop();
                            if (stackAutomatas.Count == 0)
                            {
                                stackAutomatas.Push(autAux);
                            }
                            else
                            {
                                automata = new AFN(stackAutomatas.Pop(), autAux);
                                stackAutomatas.Push(automata);
                            }
                            break;
                        case '|':
                            autAux = stackAutomatas.Pop();
                            if (stackAutomatas.Count == 0)
                            {
                                stackAutomatas.Push(autAux);
                            }
                            else
                            {
                                automata = AFN.SeleccionAlternativa(stackAutomatas.Pop(), autAux);
                                stackAutomatas.Push(automata);
                            }
                            break;
                    }

                }


            }
            if (stackAutomatas.Count() != 1)
            {
                MessageBox.Show("Error aun quedan automatas en la pila #" + stackAutomatas.Count());
            }
            alfabeto.Add('ε');
            // alfabeto.Sort();
            alfabeto = alfabeto.Distinct().ToList();
            return stackAutomatas.Pop();
        }


        private void ContarEstadosYtransi(AFN automata)
        {
            int TransicionesEps = 0, estados = 0;
            foreach (Transition x in automata.MisTransiciones)
            {
                if (x.nombre == 'ε')
                {
                    TransicionesEps++;
                }
                if (x.destino >= estados)
                {
                    estados = x.destino;
                }
                if (x.origen >= estados)
                {
                    estados = x.origen;
                }
            }
            estados++;
            N_Estados.Text = estados.ToString();
            N_Epsi.Text = TransicionesEps.ToString();
        }
        private int ContarEstadosYtransi2(AFN automata)
        {
            int TransicionesEps = 0, estados = 0;
            foreach (Transition x in automata.MisTransiciones)
            {
                if (x.nombre == 'ε')
                {
                    TransicionesEps++;
                }
                if (x.destino >= estados)
                {
                    estados = x.destino;
                }
                if (x.origen >= estados)
                {
                    estados = x.origen;
                }
            }
            estados++;
            return estados;
        }
        private void creartabla(AFN automata, List<char> alfabeto)
        {
            dataGridView2.Columns.Add("Estados", "Estados");
            foreach (char datoColumna in alfabeto)
            {
                dataGridView2.Columns.Add(datoColumna.ToString(), datoColumna.ToString());
            }
            for (int i = 0; i < int.Parse(N_Estados.Text.ToString()); i++)
            {
                dataGridView2.Rows.Add(i.ToString());
            }

            for (int i = 0; i < int.Parse(N_Estados.Text.ToString()); i++)
            {
                for (int j = 0; j < alfabeto.Count; j++)
                {
                    int estadoOrigen = i;
                    char simbolo = alfabeto[j];

                    dataGridView2.Rows[i].Cells[j + 1].Value = automata.Conecta(simbolo, estadoOrigen);
                }
            }
        }
        //************************************************ AFD *************************************************************************
        private void button3_Click(object sender, EventArgs e) // Boton AFD
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            AFN automata = ConvertToAFN(txtOutput.Text);
            AFN automataafd = ConvertToAFD(automata);

            automataafd.LlenaListaEstados2();
            creartablaafd(automataafd, alfabeto);
            NumAFD.Text = automataafd.ListaEstado2.Count.ToString();
        }

        private AFN ConvertToAFD(AFN automata)
        {
            AFN AFD = new  AFN ();
            List<Transition> union = new List<Transition>();
            List<AFN> DEstados = new List<AFN>();
            List<Transition> Simbolos = new List<Transition>();
            List<char> alfa = new List<char>();
            int j = 0;
            int cantidadTrans = 0;
            List<AFN> aux = new List<AFN>();
            bool flag = false;
            DEstados.Add(new AFN(CEpsilon(automata, 0, 0), 'A'));
            automata.LlenaListaEstados();

            if (DEstados[0].ListaEstado.Contains(int.Parse(N_Estados.Text)))
            {
                DEstados[0].aceptados(DEstados[0].nombreEstado);
            }


            while (VisitadosDE(DEstados) == false)
            {
                DEstados[j].visitado = true;
                for (int i = 0; i < DEstados[j].ListaEstado.Count; i++)
                {
                    Simbolos.AddRange(noTerminales(automata, DEstados[j].ListaEstado[i]));

                }
                foreach (Transition q in Simbolos)
                {
                    alfa.Add(q.nombre);

                }
                alfa = alfa.Distinct().ToList();


                for (int k = 0; k < alfa.Count; k++)
                {
                    foreach (Transition x in Simbolos)
                    {
                        if (x.nombre == alfa[k])
                        {
                            // aux.Add( new Automata(CEpsilon(automata, x.destino,0), (char)(DEstados[j].nombreEstado + l)));
                            aux.Add(new AFN(CEpsilon(automata, x.destino, 0), (char)(DEstados[DEstados.Count - 1].nombreEstado + 1)));
                            cantidadTrans++;
                        }
                    }
                    if (cantidadTrans > 1)
                    {
                        foreach (AFN s in aux)
                        {
                            union.AddRange(s.MisTransiciones);
                        }
                        union = union.Distinct().ToList();
                        AFN aux1 = new AFN(union, aux[0].nombreEstado);
                        foreach (AFN y in DEstados)
                        {

                            if (AFN.compararaut(y, aux1))
                            {
                                AFD.MisTransiciones.Add(new Transition(alfa[k], y.nombreEstado, DEstados[j].nombreEstado));
                                flag = true;
                            }
                        }
                        if (!flag)
                        {

                            AFD.MisTransiciones.Add(new Transition(alfa[k], aux[0].nombreEstado, DEstados[j].nombreEstado));
                            DEstados.Add(aux1);
                        }
                    }
                    else if (cantidadTrans == 1)
                    {
                        foreach (AFN y in DEstados)
                        {

                            if (AFN.compararaut(y, aux[0]))
                            {
                                AFD.MisTransiciones.Add(new Transition(alfa[k], y.nombreEstado, DEstados[j].nombreEstado));
                                flag = true;
                            }
                        }
                        if (!flag)
                        {


                            AFD.MisTransiciones.Add(new Transition(alfa[k], aux[0].nombreEstado, DEstados[j].nombreEstado));
                            DEstados.Add(aux[0]);
                        }
                    }
                    cantidadTrans = 0;
                    union.Clear();
                    aux.Clear();
                }

                j++;
                flag = false;
                Simbolos.Clear();
            }
            return AFD;///Cambiar
        }

        private void creartablaafd(AFN automata, List<char> alfabeto)
        {
            dataGridView1.Columns.Add("Estados", "Estados");
            foreach (char datoColumna in alfabeto)
            {
                if (datoColumna == 'ε') break;
                dataGridView1.Columns.Add(datoColumna.ToString(), datoColumna.ToString());
            }
            for (int i = 0; i < automata.ListaEstado2.Count; i++)
            {
                dataGridView1.Rows.Add(automata.ListaEstado2[i].ToString());
            }

            for (int i = 0; i < automata.ListaEstado2.Count; i++)
            {
                for (int j = 0; j < alfabeto.Count - 1; j++)
                {
                    char simbolo = alfabeto[j];
                    dataGridView1.Rows[i].Cells[j + 1].Value = automata.Conecta2(simbolo, automata.ListaEstado2[i]);
                }
            }
        }

        private List<Transition> CEpsilon(AFN automata, int nombre, int i)
        {
            List<Transition> Estados = new List<Transition>();
            List<Transition> Estados2 = new List<Transition>();
            i++;

            foreach (Transition t in automata.MisTransiciones)
            {

                if (t.nombre == 'ε' && t.origen == nombre)
                {
                    Estados.Add(t);
                }
            }

            foreach (Transition t in Estados)
            {

                Estados2.AddRange(CEpsilon(automata, t.destino, i));
            }

            Estados.AddRange(Estados2);
            if (Estados.Count == 0 && i == 1)
            {
                foreach (Transition t in automata.MisTransiciones)
                {

                    if (t.nombre != 'ε' && t.origen == nombre)
                    {
                        t.Unitario = true;
                        Estados.Add(t);

                        break;
                    }
                }
            }
            if (Estados.Count == 0 && i == 1)
            {
                foreach (Transition t in automata.MisTransiciones)
                {

                    if (t.nombre != 'ε' && t.destino == nombre)
                    {
                        t.Unitario = false;
                        t.UnitarioTP2 = true;
                        Estados.Add(t);

                        break;
                    }
                }
            }

            if (Estados.Count == 0 && i == 1)
            {
                foreach (Transition t in automata.MisTransiciones)
                {

                    if (t.nombre != 'ε' && t.destino == nombre)
                    {
                        Estados.Add(t);
                        break;
                    }
                }
            }

            return Estados;
        }

        private bool VisitadosDE(List<AFN> DEstados)
        {

            foreach (AFN t in DEstados)
            {
                if (t.visitado == false)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Transition> noTerminales(AFN automata, int nombre)
        {
            List<Transition> Estados = new List<Transition>();
            List<Transition> Estados2 = new List<Transition>();

            foreach (Transition t in automata.MisTransiciones)
            {
                if (t.nombre != 'ε' && t.origen == nombre)
                {
                    Estados.Add(t);
                }
            }

            foreach (Transition t in Estados)
            {
                Estados2.AddRange(noTerminales(automata, t.destino));
            }

            Estados.AddRange(Estados2);
            Estados = Estados.Distinct().ToList();
            return Estados;
        }

        private void validar_Click(object sender, EventArgs e)
        {
            Valida.Text = "";
            Valida.ForeColor = Color.Black;
            char[] lex = lexema.Text.ToCharArray();
            bool estado = false;

            estado = AnalisisLexema(lex, 0, 0, 1);

            if (estado == false)
            {
                Valida.Text = "Lexema no válido";
                Valida.ForeColor = Color.Red;
            }
            else
            {
                Valida.Text = "Lexema válido";
                Valida.ForeColor = Color.Green;
            }
        }
        private bool AnalisisLexema(char[] cadena, int I, int origen, int tipo)
        {
            bool respuesta = false;
            if (I >= cadena.Length)
            {
                tipo = 2;
            }
            foreach (Transition x in AFD.MisTransiciones)
            {
                if (respuesta == true)
                {
                    return respuesta;
                }
                if (tipo == 2)
                {
                    if (x.origen == origen && x.nombre == 'ε')
                    {
                        respuesta = AnalisisLexema(cadena, I, x.destino, tipo);
                    }
                }
                else
                {
                    if (x.origen == origen && x.nombre == 'ε')
                    {
                        respuesta = AnalisisLexema(cadena, I, x.destino, tipo);
                    }
                    if (x.origen == origen && x.nombre == cadena[I])
                    {
                        respuesta = AnalisisLexema(cadena, I + 1, x.destino, tipo);
                    }
                }
                if (origen == int.Parse(N_Estados.Text) - 1)
                {
                    if (I != cadena.Length)
                    {
                        return false;
                    }
                    else
                        return true;
                }

            }
            return respuesta;
        }

        private void lexema_TextChanged(object sender, EventArgs e)
        {
            Valida.Text = " ";
        }
    }
}