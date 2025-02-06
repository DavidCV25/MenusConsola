/// <summary>
/// Gestiona las opciones del menú
/// </summary>
public class Opcion
{
    string _nombre = "";
    Action _accion = new Action(Console.WriteLine);

    /// <summary>
    /// Nombre de la opción
    /// </summary>
    public string Nombre
    {
        get => _nombre;
        set => _nombre = value;
    }

    /// <summary>
    /// Accion que realiza la opción
    /// </summary>
    public Action Accion
    {
        get => _accion;
        set => _accion = value;
    }

    /// <summary>
    /// Constructor básico
    /// </summary>
    /// <param name="nombre">Nombre de la accion</param>
    /// <param name="accion">Accion que realiza</param>
    public Opcion(string nombre, Action accion)
    {
        Nombre = nombre;
        Accion = accion;
    }
}