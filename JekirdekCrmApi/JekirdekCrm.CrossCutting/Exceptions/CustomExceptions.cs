using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.CrossCutting.Exceptions
{
    //Yönetilen Hatalar

    /// <summary>
    /// Bulunamaz İse Hatası
    /// </summary>
    /// <param name="message"></param>
    public class NotFoundException(string message) : Exception(message) {}

    /// <summary>
    /// Benzer Var İse Hatası
    /// </summary>
    /// <param name="message"></param>
    public class ConflictException(string message) : Exception(message) {}

    /// <summary>
    /// Şifre Yanlış İse Hatası
    /// </summary>
    /// <param name="message"></param>
    public class PasswordErrorException(string message) : Exception(message) { }

    /// <summary>
    /// Şifre Yanlış İse Hatası
    /// </summary>
    /// <param name="message"></param>
    public class ValidFilterKey(string message) : Exception(message) { }

}
