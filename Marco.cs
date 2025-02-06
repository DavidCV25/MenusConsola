namespace MenusConsola;

/// <summary>
/// Clase Marco, gestiona el contenedor del menu
/// </summary>
public class Marco
{
    #region Constantes

        const int BARRAHORIZONTAL = 0;
        const int BARRAVERTICAL = 1;
        const int ESQUINAARRIBAIZQUIERDA = 2;
        const int ESQUINAARRIBADERECHA = 3;
        const int ESQUINAABAJOIZQUIERDA = 4;
        const int ESQUINAABAJODERECHA = 5;

    #endregion
    
    #region Campos
    
        int _x1 = 0;
        int _y1 = 0;
        int _ancho = 0;
        int _alto = 0;
        int _contarLinea = 1;
        TipoTrazado _tipoTrazo = TipoTrazado.Simple;
        ConsoleColor _colorMarco = ConsoleColor.White;
        ConsoleColor _colorFondo = ConsoleColor.Black;
        char[] _bordesSimples = {'─', '│', '┌', '┐', '└', '┘'};
        char[] _bordesDobles  = {'═', '║', '╔', '╗', '╚', '╝'};

    #endregion
    
    #region Propiedades
    
        /// <summary>
        /// Posición inicial en el eje x
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">En caso de recibir un valor mayor que el de la consola o menor que 0</exception>
        public int X1
        {
            get => _x1;
            set
            {
                if (value > Console.BufferWidth || value < 0)
                    throw new ArgumentOutOfRangeException("El valor de X1 no puede ser mayor que el ancho de la página o menor que 0");
                _x1 = value;
            }
        }

        /// <summary>
        /// Posición inicial en el eje y
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">En caso de recibir un valor mayor que el de la consola o menor que 0</exception>
        public int Y1
        {
            get => _y1;
            set
            {
                if (value > Console.BufferHeight)
                    throw new ArgumentOutOfRangeException("El valor de Y1 no puede ser mayor que el largo de la página");
                _y1 = value;
            }
        }

        /// <summary>
        /// Ancho del menú, en caso de ser negativo el valor es 0
        /// </summary>
        public int Ancho
        {
            get => _ancho;
            set
            {
                if (value < 0)
                    _ancho = 0;
                else
                    _ancho = value;
            }
        }

        /// <summary>
        /// Alto del menú, en caso de ser negativo el valor es 0
        /// </summary>
        public int Alto
        {
            get => _alto;
            set
            {
                if (value < 0)
                    _alto = 0;
                else
                    _alto = value;
            }
        }

        /// <summary>
        /// El tipo de trazo en el que se realiza el menú
        /// </summary>
        public TipoTrazado TipoTrazo
        {
            get => _tipoTrazo;
            set => _tipoTrazo = value;
        }

        /// <summary>
        /// Color del marco
        /// </summary>
        public ConsoleColor ColorMarco
        {
            get => _colorMarco;
            set => _colorMarco = value;
        }
        
        /// <summary>
        /// Color del fondo de la consola
        /// </summary>
        public ConsoleColor ColorFondo
        {
            get => _colorFondo;
            set => _colorFondo = value;
        }

    #endregion
    
    #region Constructores
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">Posicion inicial en x</param>
        /// <param name="y1">Posicion inicial en y</param>
        /// <param name="ancho">Ancho del marco</param>
        /// <param name="alto">Alto del marco</param>
        /// <param name="tipoTrazo">Tipo de trazo</param>
        /// <param name="colorFondo">Color del fondo</param>
        /// <param name="colorMarco">Color del marco</param>
        public Marco (int x1, int y1, int ancho, int alto, TipoTrazado tipoTrazo = TipoTrazado.Simple, ConsoleColor colorMarco = ConsoleColor.White, ConsoleColor colorFondo = ConsoleColor.Black)
        {
            X1 = x1;
            Y1 = y1;
            Ancho = ancho;
            Alto = alto;
            TipoTrazo = tipoTrazo;
            ColorMarco = colorMarco;
            ColorFondo = colorFondo;
        }

    #endregion
    
    #region Métodos
    
        /// <summary>
        /// Es un constructor factory el cuál inicia un Marco centrado en la consola
        /// </summary>
        /// <param name="ancho">Ancho del marco</param>
        /// <param name="alto">Alto del marco</param>
        /// <param name="tipoTrazo">Tipo de trazo</param>
        /// <param name="colorFondo">Color del fondo</param>
        /// <param name="colorMarco">Color del marco</param>
        /// <returns>Devuelve un objeto de tipo marco que se centra en la consola</returns>
        public static Marco MarcoCentrado(int ancho, int alto, TipoTrazado tipoTrazo = TipoTrazado.Simple, ConsoleColor colorFondo = ConsoleColor.Black, ConsoleColor colorMarco = ConsoleColor.White)
        {
            return new Marco((Console.BufferWidth - ancho)/2, (Console.BufferHeight - alto)/2, ancho, alto, tipoTrazo, colorMarco, colorFondo);
        }

        /// <summary>
        /// Imprime el método por pantalla
        /// </summary>
        public void Dibujar()
        {
            char[] tipoLinea = (int)TipoTrazo == 0? _bordesSimples : _bordesDobles;
            DibujaTecho(tipoLinea[BARRAHORIZONTAL], tipoLinea[ESQUINAARRIBAIZQUIERDA], tipoLinea[ESQUINAARRIBADERECHA]);
            for (int i = 0; i < Alto - 1 && Y1 + _contarLinea < Console.BufferHeight - 2; i++)
                DibujaLinea(tipoLinea[BARRAVERTICAL]);
            DibujaSuelo(tipoLinea[BARRAHORIZONTAL], tipoLinea[ESQUINAABAJOIZQUIERDA], tipoLinea[ESQUINAABAJODERECHA]);
        }
        #region Métodos Auxiliares

            /// <summary>
            /// Imprime por pantalla la parte superior del marco
            /// </summary>
            /// <param name="suelo">caracter del suelo</param>
            /// <param name="EsqAbaIzq">caracter de la esquina superior izquierda</param>
            /// <param name="EsqAbaDer">caracter de la esquina superior derecha</param>
            public void DibujaTecho(char suelo, char EsqAbaIzq, char EsqAbaDer)
            {
                Console.SetCursorPosition(X1, Y1);
                Console.Write(EsqAbaIzq);
                for (int i = 0; i < Ancho && i < Console.BufferWidth; i++)
                    Console.Write(suelo);
                Console.Write(EsqAbaDer+"\n");
            }

            /// <summary>
            /// Imprime por pantalla la parte inferior del marco
            /// </summary>
            /// <param name="suelo">caracter del suelo</param>
            /// <param name="EsqArrIzq">caracter de la esquina inferior izquierda</param>
            /// <param name="EsqArrDer">caracter de la esquina inferior derecha</param>
            public void DibujaSuelo(char suelo, char EsqArrIzq, char EsqArrDer)
            {
                Console.SetCursorPosition(X1, Y1 + Alto >= Console.BufferHeight? Console.BufferHeight - 2 : Y1 + Alto);
                Console.Write(EsqArrIzq);
                for (int i = 0; i < Ancho && i < Console.BufferWidth; i++)
                    Console.Write(suelo);
                Console.Write(EsqArrDer+"\n");
            }

            /// <summary>
            /// Imprime por pantalla la zona media del marco
            /// </summary>
            /// <param name="pared">Caracter de la zona lateral del marco</param>
            public void DibujaLinea(char pared)
            {
                Console.SetCursorPosition(X1, Y1 + _contarLinea++);
                Console.Write(pared);
                for (int i = 0; i < Ancho && i < Console.BufferWidth; i++)
                    Console.Write(" ");
                Console.Write(pared+"\n");
            }

        #endregion
    

    #endregion
}
