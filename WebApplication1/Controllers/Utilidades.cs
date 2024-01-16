namespace WebApplication1.Controllers
{
    public static class Utilidades
    {
        public static bool ArchivoExiste(string rutaArchivo)
        {
            return File.Exists(rutaArchivo);
        }
    }
}
