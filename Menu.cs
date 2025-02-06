using MenusConsola;

/// <summary>
/// Clase Menú, que gestiona el interior de este
/// </summary>
public class Menu
{
    #region Constantes
    
        const string SELECCIONAACCION = "Selecciona una opción: ";
        const string INSTRUCCIONESUSO = "Usa los cursores para navegar por el menú.";

    #endregion
    
    #region Campos
    
        int _opcionElegida = 1;
        string _titulo = "";
        string? _mensaje = "";
        Vector2 _posicion = new Vector2(0,0);
        ConsoleColor _colorTitulo = ConsoleColor.White;
        ConsoleColor _colorOpciones = ConsoleColor.White;
        ConsoleColor _colorFondo = ConsoleColor.Black;
        List<Opcion> _opciones = new List<Opcion>();

    #endregion
    
    #region Propiedades
    
        /// <summary>
        /// El título del menú
        /// </summary>
        /// <exception cref="ArgumentException">En caso de recibir una cadena vacía</exception>
        public string Titulo
        {
            get => _titulo;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentException("El titulo no puede ser una cadena vacía.");
                _titulo = value;
            }
        }

        /// <summary>
        /// El mensaje introducido o subtitulo
        /// </summary>
        public string? Mensaje
        {
            get => _mensaje;
            set => _mensaje = value;
        }

        /// <summary>
        /// Posición del vértice superior izquierdo
        /// </summary>
        public Vector2 Posicion
        {
            get => _posicion;
            set => _posicion = value;
        }

        /// <summary>
        /// Color del título
        /// </summary>
        public ConsoleColor ColorTitulo
        {
            get => _colorTitulo;
            set => _colorTitulo = value;
        }

        /// <summary>
        /// Color de las opciones
        /// </summary>
        public ConsoleColor ColorOpciones
        {
            get => _colorOpciones;
            set => _colorOpciones = value;
        }

        /// <summary>
        /// Color del fondo
        /// </summary>
        public ConsoleColor ColorFondo
        {
            get => _colorFondo;
            set => _colorFondo = value;
        }

    #endregion
    
    #region Constructores
    
        /// <summary>
        /// Constructor básico
        /// </summary>
        /// <param name="titulo">Título</param>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="posicion">Posicion</param>
        /// <param name="colorTitulo">Color Titulo</param>
        /// <param name="colorOpciones">Color Opciones</param>
        /// <param name="colorFondo">Color Fondo</param>
        public Menu(string titulo, string? mensaje = null, Vector2? posicion = null, ConsoleColor colorTitulo = ConsoleColor.White, ConsoleColor colorOpciones = ConsoleColor.White ,ConsoleColor colorFondo = ConsoleColor.Black)
        {
            Titulo = titulo;
            Mensaje = mensaje;
            Posicion = posicion == null? new Vector2 (0,0) : (Vector2)posicion; 
            ColorTitulo = colorTitulo;
            ColorOpciones = colorOpciones;
            ColorFondo = colorFondo;
        }

    #endregion
    
    #region Métodos
    
        /// <summary>
        /// El método incluye una opcion al menú
        /// </summary>
        /// <param name="nombreOpcion">El nombre de la opción a introducir</param>
        /// <param name="accion">Que realiza la opción</param>
        /// <returns>Posicion asignada a la opcion</returns>
        public int IncluirOpcion(string nombreOpcion, Action accion)
        {
            _opciones.Add(new Opcion(nombreOpcion, accion));
            return _opciones.Count;
        }

        /// <summary>
        /// Pinta por pantalla el menú
        /// </summary>
        public void Dibujar()
        {
            Console.Clear();
            int mayorTamano = CadenaMayorLongitud(), fila = Posicion.Y;
            
            var marco = new Marco(Posicion.X, Posicion.Y, mayorTamano, 9 + _opciones.Count, TipoTrazado.Simple, ColorTitulo, ColorFondo);
            marco.Dibujar();
            
            Console.SetCursorPosition(((mayorTamano/2)-(Titulo.Length/2)) + Posicion.X, fila);
            Console.Write($" {Titulo} ");
            
            if(Mensaje != null)
            {
                Console.SetCursorPosition((mayorTamano/2)-(Mensaje.Length/2) + Posicion.X, ++fila);
                Console.Write($" {Mensaje} ");
                fila++;
            }

            Console.SetCursorPosition((mayorTamano/10) + Posicion.X, ++fila);
            Console.Write($"{SELECCIONAACCION}");
            fila++;
            
            for (int i = 0; i < _opciones.Count; i++)
            {
                Console.SetCursorPosition((mayorTamano/5) + Posicion.X, ++fila);
                if (i + 1 == _opcionElegida)
                    Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write($"{i + 1}.");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($" {_opciones[i].Nombre}");
            }
            fila++;
            
            Console.SetCursorPosition((mayorTamano/10) + Posicion.X, ++fila);
            Console.Write($"{INSTRUCCIONESUSO}");
        }

        /// <summary>
        /// Pinta el menú y permite interactuar con este a través de las flechas del teclado, en caso de querer elegir la opcion seleccionar la tecla enter
        /// </summary>
        /// <returns>Opción seleccionada</returns>
        public int MostrarMenu()
        {
            ConsoleKeyInfo tecla;
            do
            {
                this.Dibujar();
                tecla = Console.ReadKey();
                if(tecla.Key == ConsoleKey.UpArrow && _opcionElegida < _opciones.Count)
                {
                    _opcionElegida++;
                }
                else if(tecla.Key == ConsoleKey.DownArrow && _opcionElegida > 1)
                {
                    _opcionElegida--;
                }
            } while (tecla.Key != ConsoleKey.Enter);
            Console.Clear();
            return _opcionElegida;
        }

        #region Métodos Auxiliares

            /// <summary>
            /// Devuelve la cadena de mayor longitud y le añade 10
            /// </summary>
            /// <returns>Entero con el valor del tamaño de dicha cadena</returns>
            int CadenaMayorLongitud()
            {
                int mayorTamano = Titulo.Length;
                if(Mensaje != null && Mensaje.Length > mayorTamano) mayorTamano = Mensaje.Length;
                foreach (var item in _opciones)
                    if(item.Nombre.Length > mayorTamano) mayorTamano = item.Nombre.Length; 
                if(INSTRUCCIONESUSO.Length > mayorTamano) mayorTamano = INSTRUCCIONESUSO.Length;
                
                return mayorTamano + 10;
            }

        #endregion
    #endregion
}




