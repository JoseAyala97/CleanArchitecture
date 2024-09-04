namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //hereda o hacer referencia a la clase padre 
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) no fue encontrado")
        {
            
        }
    }
}
