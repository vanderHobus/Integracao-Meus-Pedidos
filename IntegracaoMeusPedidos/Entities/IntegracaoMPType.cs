namespace IntegracaoMeusPedidos.Entities
{
    public abstract class IntegracaoMPType<T>
    {
        abstract internal string Path();
        abstract internal T ConvertJson(object obj);
    }
}
