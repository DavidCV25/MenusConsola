/// <summary>
/// Gestiona la posicion inicial del menú
/// </summary>
public class Vector2
{
    int _x = 0;
    int _y = 0;

    /// <summary>
    /// Posicion X del vector
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">En caso de recibir una valor negativo</exception>
    public int X
    {
        get => _x;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException("La posición X no puede ser negativa.");
        }
    }
    /// <summary>
    /// Posicion Y del vector
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">En caso de recibir una valor negativo</exception>
    public int Y
    {
        get => _y;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException("La posición Y no puede ser negativa.");
        }
    }

    /// <summary>
    /// Constructor básico
    /// </summary>
    /// <param name="x">Eje x</param>
    /// <param name="y">Eje y</param>
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
}