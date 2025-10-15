using System;
using System.Data.SqlClient;

namespace Test2;

class Program
{
    static void Main(string[] args)
    {
        //generamos las instancias de las clases
        Producto prod1 = new Producto();
        prod1.id = 1;
        prod1.descripcion = "PROD1";
        prod1.preciounitario = 1300;

        Producto prod2 = new Producto(2, "PROD2", 1500);

        bool Resultado = prod1.Altas(prod1);

        if (!Resultado)
        {
            //mensaje de error
        }
        Cliente clie1 = new Cliente();
        clie1.dni = 21835495;
        clie1.nombre = "Francisco";

        Factura fac1 = new Factura();

        fac1.nro = 1;
        fac1.cliente = clie1;

        DetalleFactura renglon1 = new DetalleFactura();
        renglon1.idfila = 1;
        renglon1.nroFactura = fac1.nro;
        renglon1.producto = prod1;
        renglon1.cantidad = 3;

        DetalleFactura renglon2 = new DetalleFactura();
        renglon2.idfila = 2;
        renglon2.nroFactura = fac1.nro;
        renglon2.producto = prod2;
        renglon2.cantidad = 10;

        fac1.detalle.Add(renglon1);
        fac1.detalle.Add(renglon2);

        fac1.Altas(fac1);

        string connStr = "Server=PCI;Database=TPPII;User Id=sa;Password=987123;TrustServerCertificate=True;";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            Console.WriteLine("conex con exito");

            try
            {
                string addFactura = @"INSERT INTO Factura (id, fecha, cliente_dni)
                              VALUES (@id, @fecha, @cliente_dni)";

                using (SqlCommand cmd = new SqlCommand(addFactura, conn))
                {
                    cmd.Parameters.AddWithValue("@id", fac1.nro);
                    cmd.Parameters.AddWithValue("@fecha", fac1.fecha);
                    cmd.Parameters.AddWithValue("@cliente_dni", fac1.cliente.dni);
                    cmd.ExecuteNonQuery();
                }

                string addDetalle = @"INSERT INTO DetalleFactura (factura_id, producto_id, cantidad)
                              VALUES (@factura_id, @producto_id, @cantidad)";

                foreach (var detalle in fac1.detalle)
                {
                    using (SqlCommand cmd = new SqlCommand(addDetalle, conn))
                    {
                        //cmd.Parameters.AddWithValue("@id", detalle.idfila);
                        cmd.Parameters.AddWithValue("@factura_id", detalle.nroFactura);
                        cmd.Parameters.AddWithValue("@producto_id", detalle.producto.id);
                        cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);

                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("guardado correctamente");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                //throw new Exception("Error");
            }
        }

        //Console.WriteLine(fac1.CalcularTotal());

        // Console.WriteLine(renglon1.CalcularSubTotal());
        // Console.WriteLine(renglon2.CalcularSubTotal());
    }
}
