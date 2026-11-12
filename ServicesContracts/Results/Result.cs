using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServicesContracts.Results;

public class Result<T>
{
    public bool Sucesso { get; }
    public T? Objeto { get; }
    public string? Erro { get; }

    private Result(bool sucesso, T? objeto, string? erro)
    {
        Sucesso = sucesso;
        Objeto = objeto;
        Erro = erro;
    }

    public static Result<T> Ok(T objeto) => new(true, objeto, null);
    public static Result<T> Falha(string erro) => new (false, default, erro);
}