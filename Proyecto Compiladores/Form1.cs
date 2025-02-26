using System;
using System.Collections.Generic;
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
    }
}