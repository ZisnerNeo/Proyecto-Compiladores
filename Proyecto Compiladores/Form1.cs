using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Proyecto_Compiladores
{
    public partial class Form1 : Form
    {

        bool band = false;
        List<Produccion> gramaticaTINY = new List<Produccion>
        {
            new Produccion("programa'", new List<string> { "programa" }),
            new Produccion("programa", new List<string> { "secuencia-sent" }),
            new Produccion("secuencia-sent", new List<string> { "secuencia-sent", ";", "sentencia" }),
            new Produccion("secuencia-sent", new List<string> { "sentencia" }),
            new Produccion("sentencia", new List<string> { "sent-if" }),
            new Produccion("sentencia", new List<string> { "sent-repeat" }),
            new Produccion("sentencia", new List<string> { "sent-assign" }),
            new Produccion("sentencia", new List<string> { "sent-read" }),
            new Produccion("sentencia", new List<string> { "sent-write" }),
            new Produccion("sent-if", new List<string> { "if", "exp", "then", "secuencia-sent", "end" }),
            new Produccion("sent-if", new List<string> { "if", "exp", "then", "secuencia-sent", "else", "secuencia-sent", "end" }),
            new Produccion("sent-repeat", new List<string> { "repeat", "secuencia-sent", "until", "exp" }),
            new Produccion("sent-assign", new List<string> { "identificador", ":=", "exp" }),
            new Produccion("sent-read", new List<string> { "read", "identificador" }),
            new Produccion("sent-write", new List<string> { "write", "exp" }),
            new Produccion("exp", new List<string> { "exp-simple", "op-comp", "exp-simple" }),
            new Produccion("exp", new List<string> { "exp-simple" }),
            new Produccion("op-comp", new List<string> { "<" }),
            new Produccion("op-comp", new List<string> { ">" }),
            new Produccion("op-comp", new List<string> { "=" }),
            new Produccion("exp-simple", new List<string> { "exp-simple", "opsuma", "term" }),
            new Produccion("exp-simple", new List<string> { "term" }),
            new Produccion("opsuma", new List<string> { "+" }),
            new Produccion("opsuma", new List<string> { "-" }),
            new Produccion("term", new List<string> { "term", "opmult", "factor" }),
            new Produccion("term", new List<string> { "factor" }),
            new Produccion("opmult", new List<string> { "*" }),
            new Produccion("opmult", new List<string> { "/" }),
            new Produccion("factor", new List<string> { "(", "exp", ")" }),
            new Produccion("factor", new List<string> { "numero" }),
            new Produccion("factor", new List<string> { "identificador" }),
        };


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
            AFN AFD = new AFN();
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
        private void ClasificaTokens_Click(object sender, EventArgs e)
        {
            ERRORLINE.Text = "";
            string texto = tokensTxt.Text; // Entra el codigo 
            string idPosfija = ConvertToPostfix(ConvierteExplicita(identificadorTxt.Text));
            AFN automataID = ConvertToAFN(idPosfija);
            string idNUM = ConvertToPostfix(ConvierteExplicita(NumeroTxt.Text));
            AFN automataNum = ConvertToAFN(idNUM);
            ERRORLINE.Visible = false;
            ERROR.Visible = false;
            errorlexico = false;
            // Obtener tokens
            string[] palabras = texto.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> listaPalabras = new List<string>(palabras);
            int i = 0;
            DataTable tablaTokens = new DataTable();
            tablaTokens.Columns.Add("Nombre");
            tablaTokens.Columns.Add("Token");

            foreach (string token in listaPalabras)
            {

                if (palabraReservada.ContainsKey(token))
                {
                    tablaTokens.Rows.Add(token, token);
                }
                else if (simbolosEspeciales.ContainsKey(token))
                {
                    tablaTokens.Rows.Add(token, token);
                }
                else if (COMPRUEBALEXEMA(token.ToCharArray(), 0, 0, 1, automataID))
                {
                    tablaTokens.Rows.Add(token, "identificador");
                }
                else if (COMPRUEBALEXEMA(token.ToCharArray(), 0, 0, 1, automataNum))
                {
                    tablaTokens.Rows.Add(token, "número");
                }
                else
                {
                    errorlexico = true;
                    tablaTokens.Rows.Add(token, "error");
                    ERRORLINEA(i);
                }
                i++;
            }
            dataGridviewTokens.DataSource = tablaTokens;

            foreach (DataGridViewRow fila in dataGridviewTokens.Rows)
            {
                // Obtener la celda de la segunda columna
                DataGridViewCell celda = fila.Cells[1]; // Índice de la segunda columna

                // Verificar si la celda contiene el texto "error" y establecer el color de fondo correspondiente
                if (celda.Value != null && celda.Value.ToString().Equals("error", StringComparison.OrdinalIgnoreCase))
                {
                    celda.Style.ForeColor = Color.Red;
                }
                else
                    celda.Style.ForeColor = Color.Green;
            }
        }
        //Clasificacion de Lexemas 
        private void clasificaTokenBtn_Click(object sender, EventArgs e)
        {
            ERRORLINE.Text = "";
            string texto = tokensTxt.Text; // Entra el codigo 
            string idPosfija = ConvertToPostfix(ConvierteExplicita(identificadorTxt.Text));
            AFN automataID = ConvertToAFN(idPosfija);
            string idNUM = ConvertToPostfix(ConvierteExplicita(NumeroTxt.Text));
            AFN automataNum = ConvertToAFN(idNUM);
            ERRORLINE.Visible = false;
            ERROR.Visible = false;
            errorlexico = false;
            // Obtener tokens
            string[] palabras = texto.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> listaPalabras = new List<string>(palabras);
            int i = 0;
            DataTable tablaTokens = new DataTable();
            tablaTokens.Columns.Add("Nombre");
            tablaTokens.Columns.Add("Token");




            foreach (string token in listaPalabras)
            {

                if (palabraReservada.ContainsKey(token))
                {
                    tablaTokens.Rows.Add(token, token);
                }
                else if (simbolosEspeciales.ContainsKey(token))
                {
                    tablaTokens.Rows.Add(token, token);
                }
                else if (COMPRUEBALEXEMA(token.ToCharArray(), 0, 0, 1, automataID))
                {
                    tablaTokens.Rows.Add(token, "identificador");
                }
                else if (COMPRUEBALEXEMA(token.ToCharArray(), 0, 0, 1, automataNum))
                {
                    tablaTokens.Rows.Add(token, "numero");
                }
                else
                {
                    errorlexico = true;
                    tablaTokens.Rows.Add(token, "error");
                    //ERRORLINEA(i);
                    int linea = ObtenerLineaDeToken(i, texto);
                    richTextBoxErrores.SelectionStart = richTextBoxErrores.TextLength;
                    richTextBoxErrores.SelectionLength = 0;
                    richTextBoxErrores.SelectionColor = Color.Red;
                    richTextBoxErrores.AppendText($"Línea {linea}: '{token}' no se reconoce.\n");
                    richTextBoxErrores.SelectionColor = richTextBoxErrores.ForeColor; // Restablece color

                }
                i++;
            }
            dataGridviewTokens.DataSource = tablaTokens;

            foreach (DataGridViewRow fila in dataGridviewTokens.Rows)
            {
                // Obtener la celda de la segunda columna
                DataGridViewCell celda = fila.Cells[1]; // Índice de la segunda columna

                // Verificar si la celda contiene el texto "error" y establecer el color de fondo correspondiente
                if (celda.Value != null && celda.Value.ToString().Equals("error", StringComparison.OrdinalIgnoreCase))
                {
                    celda.Style.ForeColor = Color.Red;
                }
                else
                    celda.Style.ForeColor = Color.Green;
            }
            //Implementacion Arbol
            // Obtener tokens clasificados
            List<string> tokens = new List<string>();
            foreach (DataGridViewRow fila in dataGridviewTokens.Rows)
            {
                if (fila.Cells[1].Value != null && fila.Cells[1].Value.ToString() != "error")
                {
                    tokens.Add(fila.Cells[1].Value.ToString());
                }
            }

            // Reconstruir parser y autómata LR(0)
            LR0Parser parser = new LR0Parser(gramaticaTINY);
            List<List<ElementoLR0>> estados = parser.ConstruirAutomataLR0();

            ConstruirArbolSintactico(tokens, parser, estados);

        }

        private string ConvierteExplicita(string ER)
        {
            bool vacio = true;
            bool AntDigito = false;
            string explicito = "";

            int j = 0;
            for (int i = 0; i < ER.Length; i++)
            {
                char c = ER[i];

                if (char.IsLetterOrDigit(c))
                {
                    if (vacio)
                    {
                        explicito += c;

                        vacio = false;
                    }
                    else if (AntDigito)
                    {
                        explicito += "&" + c;

                    }
                    /*if(ER[i - 1] == ']' || ER[i - 1] == ']') {
                        explicito += "&";
                    }*/
                    else
                    {
                        explicito += c;

                    }
                    AntDigito = true;
                }
                else
                {
                    switch (c)
                    {
                        case '*':
                            explicito += c;
                            AntDigito = true;
                            break;
                        case '+':
                            explicito += c;
                            AntDigito = true;
                            break;
                        case '?':
                            explicito += c;
                            AntDigito = true;
                            break;
                        case '|':
                            explicito += c;
                            AntDigito = false;
                            vacio = true;
                            break;
                        case '[':
                            j = i + 1;
                            if (!vacio && ER[i - 1] != '(')
                            {
                                explicito += "&"; //
                            }
                            explicito += "(";
                            if (char.IsLetter(ER[i + 1]) && ER[i + 2] != '-')
                            {
                                do
                                {
                                    explicito += ER[j];
                                    if (ER[j + 1] != ']')
                                    {
                                        explicito += "|";
                                    }
                                    j++;
                                } while (ER[j] != ']');
                                i = j;
                            }
                            else if (ER[i + 2] == '-')
                            {
                                if (char.IsLetter(ER[i + 1]))
                                {
                                    for (char letra = ER[i + 1]; letra <= ER[i + 3]; letra++)
                                    {
                                        explicito += letra;
                                        if (letra != ER[i + 3])
                                        {
                                            explicito += "|";
                                        }
                                    }

                                }
                                else
                                {
                                    for (int x = int.Parse(ER[i + 1].ToString()); x <= int.Parse(ER[i + 3].ToString()); x++)
                                    {
                                        explicito += x;
                                        if (x != int.Parse(ER[i + 3].ToString()))
                                        {
                                            explicito += "|";
                                        }

                                    }
                                }
                                i += 4;
                            }
                            else if (char.IsLetterOrDigit(ER[i + 1]) && ER[i + 2] != '-')
                            {
                                do
                                {
                                    explicito += ER[j];
                                    if (ER[j + 1] != ']')
                                    {
                                        explicito += "|";
                                    }
                                    j++;
                                } while (ER[j] != ']');
                                i = j;
                            }
                            explicito += ")";
                            AntDigito = true;
                            vacio = false;
                            break;
                        case '(':
                            if (AntDigito)
                            {
                                explicito += "&";//
                            }
                            AntDigito = false;
                            explicito += "(";
                            j = i + 1;
                            if (CompararParentesis(ER, i))
                            {
                                break;
                            }
                            if (char.IsLetter(ER[i + 1]) && ER[i + 2] != '-')
                            {
                                do
                                {
                                    explicito += ER[j];
                                    if (ER[j + 1] != ')')
                                    {
                                        explicito += "&";//
                                    }
                                    j++;
                                } while (ER[j] != ')');
                                i = j;
                            }
                            else if (ER[i + 2] == '-')
                            {
                                if (char.IsLetter(ER[i + 1]))
                                {
                                    for (char letra = ER[i + 1]; letra <= ER[i + 3]; letra++)
                                    {
                                        explicito += letra;
                                        if (letra != ER[i + 3])
                                        {
                                            explicito += "&";//
                                        }
                                    }

                                }
                                else
                                {
                                    for (int x = int.Parse(ER[i + 1].ToString()); x <= int.Parse(ER[i + 3].ToString()); x++)
                                    {
                                        explicito += x;
                                        if (x != int.Parse(ER[i + 3].ToString()))
                                        {
                                            explicito += "&";//
                                        }

                                    }

                                }
                                i += 4;
                            }
                            explicito += ")";
                            AntDigito = true;
                            vacio = false;
                            break;
                        case ')':
                            explicito += ")";
                            break;
                        default:
                            explicito += c;
                            break;
                    }
                    //vacio = false;
                }
            }
            return explicito;
        }

        private bool COMPRUEBALEXEMA(char[] cadena, int I, int origen, int tipo, AFN AFD2)
        {
            bool respuesta = false;
            if (I >= cadena.Length)
            {
                tipo = 2;
            }
            foreach (Transition x in AFD2.MisTransiciones)
            {
                if (respuesta == true)
                {
                    return respuesta;
                }
                if (tipo == 2)
                {
                    if (x.origen == origen && x.nombre == 'ε')
                    {
                        respuesta = COMPRUEBALEXEMA(cadena, I, x.destino, tipo, AFD2);
                    }
                }
                else
                {
                    if (x.origen == origen && x.nombre == 'ε')
                    {
                        respuesta = COMPRUEBALEXEMA(cadena, I, x.destino, tipo, AFD2);
                    }
                    if (x.origen == origen && x.nombre == cadena[I])
                    {
                        respuesta = COMPRUEBALEXEMA(cadena, I + 1, x.destino, tipo, AFD2);
                    }
                }
                if (origen == ContarEstadosYtransi2(AFD2) - 1)
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

        private void ERRORLINEA(int UBI)
        {
            int linea = 0;
            string[] LINEAS = tokensTxt.Lines;
            List<string> tokensList = new List<string>();

            foreach (string line in LINEAS)
            {
                linea++;
                // Dividir la línea en tokens y añadirlos a la lista
                string[] tokens = line.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                tokensList.AddRange(tokens);
                ERROR.ForeColor = Color.Red;
                ERRORLINE.ForeColor = Color.Red;
                // Convertir la lista a un arreglo para verificar la longitud
                if (tokensList.Count > UBI)
                {

                    ERRORLINE.Text += "Línea " + linea + ". " + tokensList[UBI] + " No se Reconoce" + "\n";
                    ERROR.Text = "Se encontró uno o mas errores en el programa.";
                    break;

                }
            }

        }

        public bool CompararParentesis(string ER, int x)
        {

            do
            {
                if (ER[x] == '+' || ER[x] == '?' || ER[x] == '*' || ER[x] == '|' || ER[x] == '[')
                {
                    return false;
                }
                x++;
            } while (ER[x] == ')');
            return true;
        }



        //Cambios de LR0 6ta 
        private void LR0Boton_Click(object sender, EventArgs e)
        {
            // Cargar gramática actualizada
            

            // Construir autómata
            LR0Parser parser = new LR0Parser(gramaticaTINY);
            List<List<ElementoLR0>> estados = parser.ConstruirAutomataLR0();

            // Preparar tabla
            dataGridViewLR0.Rows.Clear();
            dataGridViewLR0.Columns.Clear();
            dataGridViewLR0.Columns.Add("Edo", "Edo");

            HashSet<string> simbolos = new HashSet<string>();
            foreach (var estado in estados)
            {
                foreach (var elem in estado)
                {
                    if (elem.Punto < elem.Produccion.Derecha.Count)
                        simbolos.Add(elem.Produccion.Derecha[elem.Punto]);
                }
            }

            //************** Llenado de Ir_A y ACCION *******************
            
            // Limpia las tablas existentes
            dataGridViewAccion.Rows.Clear();
            dataGridViewAccion.Columns.Clear();
            dataGridViewIrA.Rows.Clear();
            dataGridViewIrA.Columns.Clear();

            // Configura columnas iniciales
            dataGridViewAccion.Columns.Add("Estados", "Estados");
            dataGridViewIrA.Columns.Add("Estados", "Estados");

            // Determina terminales y no terminales desde la gramática
            HashSet<string> noTerminales = new HashSet<string>(gramaticaTINY.Select(p => p.Izquierda));
            HashSet<string> terminales = new HashSet<string>();

            foreach (var prod in gramaticaTINY)
            {
                foreach (var simbolo in prod.Derecha)
                {
                    if (!noTerminales.Contains(simbolo) && simbolo != "")
                        terminales.Add(simbolo);
                }
            }
            terminales.Add("$");

            // Agrega columnas para terminales en ACCIÓN
            foreach (string t in terminales)
            {
                dataGridViewAccion.Columns.Add(t, t);
            }

            // Agrega columnas para no terminales en IR_A
            foreach (string nt in noTerminales.Where(x => x != "A'"))
            {
                dataGridViewIrA.Columns.Add(nt, nt);
            }

            // Itera sobre los estados del autómata
            for (int i = 0; i < estados.Count; i++)
            {
                List<string> filaAccion = new List<string> { $"I{i}" };
                List<string> filaIrA = new List<string> { $"I{i}" };

                // === ACCIÓN (CORREGIDA) ===
                foreach (string t in terminales)
                {
                    var destino = parser.Goto(estados[i], t);
                    int idx = estados.FindIndex(est => est.Count == destino.Count && !est.Except(destino).Any());

                    if (idx != -1)
                    {
                        filaAccion.Add($"d{idx}");
                    }
                    else
                    {
                        // Revisión de posibles reducciones
                        var reducibles = estados[i].Where(elem => elem.Punto == elem.Produccion.Derecha.Count).ToList();
                        string accion = "";

                        foreach (var r in reducibles)
                        {
                            if (r.Produccion.Izquierda == "programa'")
                            {
                                if (t == "$")
                                {
                                    accion = "Aceptar";
                                    break;
                                }
                            }
                            else
                            {
                                int prodNum = gramaticaTINY.FindIndex(p =>
                                    p.Izquierda == r.Produccion.Izquierda &&
                                    p.Derecha.SequenceEqual(r.Produccion.Derecha));

                                if (string.IsNullOrEmpty(accion))
                                {
                                    accion = $"r{prodNum}";
                                }
                                else if (accion != $"r{prodNum}")
                                {
                                    // Conflicto reducción-reducción (solo marcamos)
                                    accion = "conflicto";
                                    break;
                                }
                            }
                        }

                        filaAccion.Add(accion);
                    }
                }


                // === IR_A ===
                foreach (string nt in noTerminales.Where(x => x != "A'"))
                {
                    var destino = parser.Goto(estados[i], nt);
                    int idx = estados.FindIndex(est => est.Count == destino.Count && !est.Except(destino).Any());
                    filaIrA.Add(idx != -1 ? idx.ToString() : "");
                }

                // Añadir filas a los DataGridView
                dataGridViewAccion.Rows.Add(filaAccion.ToArray());
                dataGridViewIrA.Rows.Add(filaIrA.ToArray());
            }

            var listaSimbolos = simbolos.OrderBy(s => s).ToList();
            foreach (var simbolo in listaSimbolos)
                dataGridViewLR0.Columns.Add(simbolo, simbolo);

            // Llenar filas
            for (int i = 0; i < estados.Count; i++)
            {
                var fila = new List<string> { $"I{i}" };
                foreach (var simbolo in listaSimbolos)
                {
                    var destino = parser.Goto(estados[i], simbolo);
                    int index = estados.FindIndex(estado => estado.Count == destino.Count && !estado.Except(destino).Any());
                    fila.Add(index != -1 ? $"I{index}" : "");
                }
                dataGridViewLR0.Rows.Add(fila.ToArray());
            }

            // Mostrar conjuntos
            textBoxEstadosLR0.Clear();
            for (int i = 0; i < estados.Count; i++)
            {
                textBoxEstadosLR0.AppendText($"I{i} = {{\r\n");
                foreach (var elem in estados[i])
                {
                    textBoxEstadosLR0.AppendText($"   {elem.MostrarElemento()}\r\n");
                }
                textBoxEstadosLR0.AppendText("}\r\n\r\n");
            }

            MessageBox.Show($"Generados {estados.Count} estados.", "Éxito");
        }

        private void ConstruirArbolSintactico(List<string> tokens, LR0Parser parser, List<List<ElementoLR0>> estados)
        {
            TreeView treeViewSintactico = treeViewLR0; // Usa el TreeView ya presente
            treeViewSintactico.Nodes.Clear();

            Stack<int> pilaEstados = new Stack<int>();
            Stack<TreeNode> pilaNodos = new Stack<TreeNode>();
            pilaEstados.Push(0);

            tokens.Add("$"); // Fin de cadena
            int indiceToken = 0;

            while (true)
            {
                string tokenActual = tokens[indiceToken];
                int estadoActual = pilaEstados.Peek();

                // Buscar columna correspondiente en tabla ACCION
                int colIndex = -1;
                for (int i = 1; i < dataGridViewAccion.Columns.Count; i++)
                {
                    if (dataGridViewAccion.Columns[i].Name == tokenActual)
                    {
                        colIndex = i;
                        break;
                    }
                }

                if (colIndex == -1)
                {
                    MessageBox.Show($"Token no reconocido: {tokenActual}", "Error sintáctico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                string accion = dataGridViewAccion.Rows[estadoActual].Cells[colIndex].Value?.ToString();

                if (string.IsNullOrEmpty(accion))
                {
                    richTextBoxErrores.SelectionStart = richTextBoxErrores.TextLength;
                    richTextBoxErrores.SelectionLength = 0;
                    richTextBoxErrores.SelectionColor = Color.Red;
                    richTextBoxErrores.AppendText("Se detectó un error sintáctico en el programa.\n");
                    richTextBoxErrores.SelectionColor = richTextBoxErrores.ForeColor;
                }


                if (accion.StartsWith("d")) // Desplazar
                {
                    int nuevoEstado = int.Parse(accion.Substring(1));
                    pilaEstados.Push(nuevoEstado);
                    pilaNodos.Push(new TreeNode(tokenActual));
                    indiceToken++;
                }
                else if (accion.StartsWith("r")) // Reducir
                {
                    int numProduccion = int.Parse(accion.Substring(1));
                    Produccion prod = parser.ObtenerProduccion(numProduccion);
                    int betaLength = prod.Derecha.Count;

                    List<TreeNode> hijos = new List<TreeNode>();

                    for (int i = 0; i < betaLength; i++)
                    {
                        pilaEstados.Pop();
                        hijos.Insert(0, pilaNodos.Pop());
                    }

                    // Buscar estado destino en tabla IrA
                    int estadoGoto = pilaEstados.Peek();
                    int colIrA = -1;
                    for (int i = 1; i < dataGridViewIrA.Columns.Count; i++)
                    {
                        if (dataGridViewIrA.Columns[i].Name == prod.Izquierda)
                        {
                            colIrA = i;
                            break;
                        }
                    }

                    string irAvalor = dataGridViewIrA.Rows[estadoGoto].Cells[colIrA].Value?.ToString();
                    if (string.IsNullOrEmpty(irAvalor))
                    {
                        MessageBox.Show("Error en tabla IrA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    pilaEstados.Push(int.Parse(irAvalor));

                    TreeNode nodoPadre = new TreeNode(prod.Izquierda);
                    foreach (var hijo in hijos)
                        nodoPadre.Nodes.Add(hijo);

                    pilaNodos.Push(nodoPadre);
                }
                else if (accion.ToLower() == "aceptar")
                {
                    treeViewSintactico.Nodes.Add(pilaNodos.Pop());
                    treeViewSintactico.ExpandAll();
                    MessageBox.Show("Análisis sintáctico correcto", "Éxito");
                    break;
                }
                else
                {
                    MessageBox.Show($"Acción no reconocida: {accion}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
        }
        private int ObtenerLineaDeToken(int tokenIndex, string texto)
        {
            string[] lineas = texto.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int contadorTokens = 0;

            for (int i = 0; i < lineas.Length; i++)
            {
                var tokensEnLinea = lineas[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                contadorTokens += tokensEnLinea.Length;
                if (tokenIndex < contadorTokens)
                    return i + 1;
            }
            return -1;
        }

        private void button4_Click(object sender, EventArgs e) //Correr todo el programa Tiny
        {
            richTextBoxErrores.Clear();
            if (band == false) { 
            LR0Boton_Click(sender,e);
            band = true;
            }
            clasificaTokenBtn_Click(sender, e);
        }

        
    }
}