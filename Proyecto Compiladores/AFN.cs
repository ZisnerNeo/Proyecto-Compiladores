using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores
{
    internal class AFN
    {
        public List<Transition> MisTransiciones = new List<Transition>();
        public char nombreEstado;
        public bool visitado = false;
        public List<int> ListaEstado = new List<int>();
        public List<char> ListaEstado2 = new List<char>();
        public string estados = string.Empty;
        public List<char> aceptacion = new List<char>();
        public AFN(List<Transition> a, char nombre)
        {
            MisTransiciones = a;
            nombreEstado = nombre;
            LlenaListaEstados();
        }

        public AFN()
        {



        }

        public void generaEstados()
        {

            estados = string.Empty;

            LlenaListaEstados();
            ListaEstado.Sort();

            foreach (int aux in ListaEstado)
            {
                estados += aux.ToString() + "-";
            }

        }

        /// <summary>
        /// Base
        /// </summary>
        public AFN(char x)
        {
            Transition a = new Transition(x);
            MisTransiciones = new List<Transition>();
            MisTransiciones.Add(a);
        }

        /// <summary>
        /// CONCATENACION
        /// </summary>
        public AFN(AFN x, AFN y)
        {
            int i = 0;
            MisTransiciones = new List<Transition>();
            foreach (Transition z in x.MisTransiciones)
            {
                MisTransiciones.Add(z);

            }
            y.MisTransiciones.RemoveAt(0);
            i = MisTransiciones.ElementAt(MisTransiciones.Count() - 1).destino; 

            foreach (Transition z in y.MisTransiciones)
            {
                Transition s = new Transition(z.nombre, z.origen + i, z.destino + i);
                MisTransiciones.Add(s);

            }
        }
        // clkPR 0061
        /// <summary>
        /// CERRADURA DE KLEENE
        /// </summary>
        public AFN(AFN x)
        {
            int i = 1;
            Transition s, y;
            MisTransiciones = new List<Transition>();
            Transition trancisionesAux = new Transition();
            MisTransiciones.Add(trancisionesAux);
            foreach (Transition z in x.MisTransiciones)
            {
                s = new Transition(z.nombre, z.origen + i, z.destino + i);
                MisTransiciones.Add(s);

            }
            s = MisTransiciones.ElementAt(MisTransiciones.Count() - 1);
            y = MisTransiciones.ElementAt(1);
            MisTransiciones.Add(new Transition(s.destino, y.origen)); 
            MisTransiciones.Add(new Transition(s.destino, s.destino + 1));
            MisTransiciones.Add(new Transition(trancisionesAux.origen, s.destino + 1));
            

        }

        public string Conecta(char x, int Origen)
        {
            string Cadena = "{";
            foreach (Transition y in MisTransiciones)
            {
                if (y.origen == Origen && y.nombre == x)
                {
                    Cadena += +y.destino + ",";
                }
            }
            Cadena += "}";

            if (Cadena.Length <= 3)
            {
                Cadena = "θ";
            }

            return Cadena;
        }
        public string Conecta2(char x, char Origen)
        {
            string Cadena = "";
            foreach (Transition y in MisTransiciones)
            {
                if (y.origen2 == Origen && y.nombre == x)
                {
                    string aux = y.destino2.ToString();
                    Cadena += aux;
                }
            }
            //Cadena += "}";

            if (Cadena.Length == 0)
            {
                Cadena = "θ";
            }

            return Cadena;
        }

        public static AFN CeroOinstancia(AFN x)
        {
            int i = 1;
            AFN aux = new AFN();
            Transition s;
            aux.MisTransiciones = new List<Transition>();
            Transition trancisionesAux = new Transition();
            aux.MisTransiciones.Add(trancisionesAux);
            foreach (Transition z in x.MisTransiciones)
            {
                s = new Transition(z.nombre, z.origen + i, z.destino + i);
                aux.MisTransiciones.Add(s);

            }
            s = aux.MisTransiciones.ElementAt(aux.MisTransiciones.Count() - 1);

            aux.MisTransiciones.Add(new Transition(s.destino, s.destino + 1));
            aux.MisTransiciones.Add(new Transition(trancisionesAux.origen, s.destino + 1));
            return aux;
        }

        public static AFN CerraduraPositiva(AFN x)
        {
            int i = 1;
            AFN aux = new AFN();
            Transition s, y;
            aux.MisTransiciones = new List<Transition>();
            Transition trancisionesAux = new Transition();
            aux.MisTransiciones.Add(trancisionesAux);
            foreach (Transition z in x.MisTransiciones)
            {
                s = new Transition(z.nombre, z.origen + i, z.destino + i);
                aux.MisTransiciones.Add(s);

            }
            y = aux.MisTransiciones.ElementAt(1);
            s = aux.MisTransiciones.ElementAt(aux.MisTransiciones.Count() - 1);
            aux.MisTransiciones.Add(new Transition(s.destino, y.origen));
            aux.MisTransiciones.Add(new Transition(s.destino, s.destino + 1));
            return aux;
        }

        public static AFN SeleccionAlternativa(AFN x, AFN j)
        {
            int i = 1;
            AFN aux = new AFN();
            Transition s, d;
            aux.MisTransiciones = new List<Transition>();
            Transition trancisionesAux = new Transition();
            aux.MisTransiciones.Add(trancisionesAux);
            foreach (Transition z in x.MisTransiciones)
            {
                s = new Transition(z.nombre, z.origen + i, z.destino + i);
                aux.MisTransiciones.Add(s);

            }
            i = aux.MisTransiciones.ElementAt(aux.MisTransiciones.Count() - 1).destino; 
            

            s = aux.MisTransiciones.ElementAt(aux.MisTransiciones.Count() - 1);

            d = new Transition(trancisionesAux.origen, i + 1);
            aux.MisTransiciones.Add(d);
            foreach (Transition z in j.MisTransiciones)
            {
                Transition a = new Transition(z.nombre, z.origen + i + 1, z.destino + i + 1);
                aux.MisTransiciones.Add(a);

            }
            d = aux.MisTransiciones.ElementAt(aux.MisTransiciones.Count() - 1);
            aux.MisTransiciones.Add(new Transition(s.destino, d.destino + 1));
            aux.MisTransiciones.Add(new Transition(d.destino, d.destino + 1));


            return aux;
        }
        public void aceptados(char x)
        {
            aceptacion.Add(x);
        }
        public void LlenaListaEstados()
        {
            foreach (Transition t in MisTransiciones)
            {
                if (t.Unitario == true)
                {
                    ListaEstado.Add(t.origen);
                }
                else if (t.UnitarioTP2 == true)
                {
                    ListaEstado.Add(t.destino);
                }
                else
                {
                    ListaEstado.Add(t.origen);
                    ListaEstado.Add(t.destino);
                }
            }
            ListaEstado = ListaEstado.Distinct().ToList(); 
           
        }
        public void LlenaListaEstados2()
        {
            foreach (Transition t in MisTransiciones)
            {
                ListaEstado2.Add(t.origen2);
                ListaEstado2.Add(t.destino2);
            }

            ListaEstado2 = ListaEstado2.Distinct().ToList();
        }

        public static bool compararaut(AFN a, AFN b)
        {

            a.generaEstados();
            b.generaEstados();

            if (a.estados == b.estados)
            {
                return true;
            }
            return false;
        }
    }
}
