using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores
{
    internal class Transition
    {
        public char nombre = 'ε';
        public int destino = 1;
        public int origen = 0;
        public char destino2;
        public char origen2;
        public bool Unitario = false;
        public bool UnitarioTP2 = false;

        public Transition()
        {

        }
        public Transition(char x)
        {
            nombre = x;
        }
        public Transition(char x, int i, int j)
        {
            nombre = x;
            destino = j;
            origen = i;
        }
        public Transition(int i, int j)
        {
            destino = j;
            origen = i;
        }

        public Transition(char nb, char d, char o)
        {
            nombre = nb;
            destino2 = d;
            origen2 = o;
        }


    }
}