using System;
using System.Security.Cryptography.X509Certificates;

namespace Test2;

public interface IABMEntidades<T>
{
    bool Altas(T entidad);
    bool Modificacion(T entidad);
    bool Bajas(T entidad);
}

//: IABMEntidades<clase>
