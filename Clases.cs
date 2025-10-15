using System;
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
        // crear y/o abrir la conexion al motor
        //grabar la factura

        var vNro = entidad.nro;
        var vFecha = entidad.fecha;
        var vCliente = entidad.cliente.dni;

        // insert into factura (campo1, campo,,) 
        // values (@nro, @fecha, @cliente)

        //lista de parametros del insert
        //@nro = vNro
        //@fecha = vFecha
        //@cliente = vCliente

        //grabar el detalle de la factura
        foreach (var item in entidad.detalle)
        {
            item.Altas(item);
        }

        //cerrar la conexion

        return true;
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

    public bool Altas(DetalleFactura entidad)
    {
        // crear y/o abrir la conexion al motor
        //grabar el detalle de factura

        var vIdFIla = entidad.idfila;
        var vNroFactura = entidad.nroFactura;
        var vProducto = entidad.producto.id;
        var vCantidad = entidad.cantidad;

        // insert into detallefactura (campo1, campo,,) 
        // values (@idfila, @nrofactura, @producto, @cantidad)

        //lista de parametros del insert
        //@idfila = vIdFila
        //@nrofactura = vNroFactura
        //@producto = vProducto
        //@cantidad = vCantidad

        //cerrar la conexion

        return true;
    }

    public bool Modificacion(DetalleFactura entidad)
    {
        throw new NotImplementedException();
    }

    public bool Bajas(DetalleFactura entidad)
    {
        throw new NotImplementedException();
    }

}
