using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Compiladores
{
    public class Produccion
    {
        public string Izquierda { get; set; }
        public List<string> Derecha { get; set; }

        public Produccion(string izquierda, List<string> derecha)
        {
            Izquierda = izquierda;
            Derecha = derecha;
        }

        public override bool Equals(object obj)
        {
            if (obj is Produccion otra)
            {
                return Izquierda == otra.Izquierda && Derecha.SequenceEqual(otra.Derecha);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Izquierda + string.Join("", Derecha)).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Izquierda} -> {string.Join(" ", Derecha)}";
        }
    }

    public class ElementoLR0
    {
        public Produccion Produccion { get; set; }
        public int Punto { get; set; }

        public ElementoLR0(Produccion produccion, int punto)
        {
            Produccion = produccion;
            Punto = punto;
        }

        public string MostrarElemento()
        {
            var parteAntes = string.Join(" ", Produccion.Derecha.Take(Punto));
            var parteDespues = string.Join(" ", Produccion.Derecha.Skip(Punto));
            return $"{Produccion.Izquierda} -> {parteAntes} • {parteDespues}".Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj is ElementoLR0 otro)
            {
                return Produccion.Equals(otro.Produccion) && Punto == otro.Punto;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Produccion.GetHashCode() + Punto).GetHashCode();
        }
    }

    public class LR0Parser
    {
        private List<Produccion> producciones;

        public LR0Parser(List<Produccion> gram)
        {
            producciones = gram;
        }

        public List<Produccion> ProduccionesDeNoTerminal(string noTerminal)
        {
            return producciones.Where(p => p.Izquierda == noTerminal).ToList();
        }
        public Produccion ObtenerProduccion(int index)
        {
            return producciones[index];
        }
        public bool EsNoTerminal(string simbolo)
        {
            return producciones.Any(p => p.Izquierda == simbolo);
        }

        public List<ElementoLR0> Cierre(List<ElementoLR0> conjunto)
        {
            var cierre = new List<ElementoLR0>(conjunto);
            bool cambio;
            do
            {
                cambio = false;
                foreach (var elemento in cierre.ToList())
                {
                    if (elemento.Punto < elemento.Produccion.Derecha.Count)
                    {
                        var simbolo = elemento.Produccion.Derecha[elemento.Punto];
                        if (EsNoTerminal(simbolo))
                        {
                            foreach (var prod in ProduccionesDeNoTerminal(simbolo))
                            {
                                var nuevo = new ElementoLR0(prod, 0);
                                if (!cierre.Contains(nuevo))
                                {
                                    cierre.Add(nuevo);
                                    cambio = true;
                                }
                            }
                        }
                    }
                }
            } while (cambio);
            return cierre;
        }

        public List<ElementoLR0> Goto(List<ElementoLR0> conjunto, string simbolo)
        {
            var resultado = new List<ElementoLR0>();
            foreach (var elemento in conjunto)
            {
                if (elemento.Punto < elemento.Produccion.Derecha.Count &&
                    elemento.Produccion.Derecha[elemento.Punto] == simbolo)
                {
                    resultado.Add(new ElementoLR0(elemento.Produccion, elemento.Punto + 1));
                }
            }
            return Cierre(resultado);
        }

        public List<string> ObtenerSimbolos(List<ElementoLR0> estado)
        {
            var simbolos = new HashSet<string>();
            foreach (var elemento in estado)
            {
                if (elemento.Punto < elemento.Produccion.Derecha.Count)
                {
                    simbolos.Add(elemento.Produccion.Derecha[elemento.Punto]);
                }
            }
            return simbolos.ToList();
        }

        public bool EstadoYaExistente(List<List<ElementoLR0>> estados, List<ElementoLR0> estado)
        {
            return estados.Any(s => s.Count == estado.Count && !s.Except(estado).Any());
        }

        public List<List<ElementoLR0>> ConstruirAutomataLR0() //Empieza la construccion
        {
            var estados = new List<List<ElementoLR0>>();
            var estadoInicial = new List<ElementoLR0>
            {
                new ElementoLR0(producciones[0], 0)
            };

            estados.Add(Cierre(estadoInicial));

            bool cambio;
            do
            {
                cambio = false;
                var nuevos = new List<List<ElementoLR0>>(estados);
                foreach (var estado in estados)
                {
                    foreach (var simbolo in ObtenerSimbolos(estado))
                    {
                        var transicion = Goto(estado, simbolo);
                        if (transicion.Count > 0 && !EstadoYaExistente(nuevos, transicion))
                        {
                            nuevos.Add(transicion);
                            cambio = true;
                        }
                    }
                }
                estados = nuevos;
            } while (cambio);

            return estados;
        }
    }
}