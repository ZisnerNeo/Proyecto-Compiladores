using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Proyecto_Compiladores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}