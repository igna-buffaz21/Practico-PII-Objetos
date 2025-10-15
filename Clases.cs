using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Test2;

public class Producto : IABMEntidades<Producto>
{
    // propiedades
    public int id { get; set; }
    public string descripcion { get; set; }
    public decimal preciounitario { get; set; }

    // Constructor por defecto (sin parámetros)
    public Producto()
    {
        id = 0;
        descripcion = string.Empty;
        preciounitario = 0m;
    }

    // Constructor con parámetros
    public Producto(int pId, string pDescripcion, decimal pPrecioUnitario)
    {
        id = pId;
        descripcion = pDescripcion;
        preciounitario = pPrecioUnitario;
    }

    public bool Altas(Producto entidad)
    {
        string cSql = "insert ito Productos (col1, col2) values (@var1, @var2)";
        // param.var1 = entidad.id;
        // param.var2 = entidad.descripcion;

        // //crear conexion al motor
        // oConec = new Conexion();
        // oConec.Execute(cSql, param);

        
        return true;
    }

    public bool Modificacion(Producto entidad)
    {
        throw new NotImplementedException();
    }

    public bool Bajas(Producto entidad)
    {
        throw new NotImplementedException();
    }

    // metodos


}

public class Cliente: IABMEntidades<Cliente>
{
    // propiedades
    public int dni { get; set; }
    public string nombre { get; set; }

    public Cliente()
    {
        dni = 0;
        nombre = string.Empty;
    }

    public bool Altas(Cliente entidad)
    {
        throw new NotImplementedException();
    }

    public bool Modificacion(Cliente entidad)
    {
        throw new NotImplementedException();
    }

    public bool Bajas(Cliente entidad)
    {
        throw new NotImplementedException();
    }

    // metodos

}

public class Factura: IABMEntidades<Factura>
{
    // propiedades
    public int nro { get; set; }
    public DateTime fecha { get; set; }
    public Cliente cliente { get; set; }
    public List< DetalleFactura > detalle { get; set; }

    public Factura()
    {
        cliente = new Cliente();
        fecha = System.DateTime.Now;
        detalle = new List<DetalleFactura>();
    }
    //metodos
    public decimal CalcularTotal()
    {
        decimal resultado = 0;

        foreach (var item in detalle)
        {
            resultado += item.CalcularSubTotal();
        }

        return resultado;
    }

    public bool Altas(Factura entidad)
    {
        string connStr = "Server=PCI;Database=TPPII;User Id=sa;Password=987123;TrustServerCertificate=True;";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            Console.WriteLine("conex abierta");

            try
            {
                string addFactura = @"INSERT INTO Factura (id, fecha, cliente_dni)
                              VALUES (@id, @fecha, @cliente_dni)";


                using (SqlCommand cmd = new SqlCommand(addFactura, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entidad.nro);
                    cmd.Parameters.AddWithValue("@fecha", entidad.fecha);
                    cmd.Parameters.AddWithValue("@cliente_dni", entidad.cliente.dni);
                    cmd.ExecuteNonQuery();
                }

                foreach (var item in entidad.detalle)
                {
                    item.Altas(item, conn);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al crear factura " + ex.Message);
                return false;
            }
        }
    }

    public bool Modificacion(Factura entidad)
    {
        throw new NotImplementedException();
    }

    public bool Bajas(Factura entidad)
    {
        throw new NotImplementedException();
    }

}

public class DetalleFactura: IABMEntidades<DetalleFactura>
{
    // propiedades
    public int idfila { get; set; }
    public int nroFactura { get; set; }
    public Producto producto { get; set; }
    public int cantidad { get; set; }

    public DetalleFactura()
    {
        producto = new Producto();
    }

    //metodos
    public decimal CalcularSubTotal()
    {
        //return 0;
        return this.producto.preciounitario * this.cantidad;
    }

    public bool Altas(DetalleFactura entidad, SqlConnection conn)
    {

        string addDetalle = @"INSERT INTO DetalleFactura (factura_id, producto_id, cantidad)
                              VALUES (@factura_id, @producto_id, @cantidad)";

        try
        {
            using (SqlCommand cmd = new SqlCommand(addDetalle, conn))
            {
                cmd.Parameters.AddWithValue("@factura_id", entidad.nroFactura);
                cmd.Parameters.AddWithValue("@producto_id", entidad.producto.id);
                cmd.Parameters.AddWithValue("@cantidad", entidad.cantidad);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("se creo todo con exito");
        }
        catch (Exception ex)
        {
            Console.WriteLine("error al insertar detalles " + ex.Message);
        }



        return true;
    }

    public bool Altas(DetalleFactura entidad) => throw new NotImplementedException();


    public bool Modificacion(DetalleFactura entidad)
    {
        throw new NotImplementedException();
    }

    public bool Bajas(DetalleFactura entidad)
    {
        throw new NotImplementedException();
    }

}
