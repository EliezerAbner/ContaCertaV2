using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using ServicesContracts;
using ServicesContracts.DTO.Movimentacao;

namespace Services;

public class MovimentacaoService : IMovimentacaoService
{
    private ContaCertaDbContext _db;

    public MovimentacaoService(ContaCertaDbContext db)
    {
        _db = db;
    }
    
    public async Task<MovimentacaoResponse> NovaMovimentacao(MovimentacaoAddRequest? novaMovimentacao)
    {
        if (novaMovimentacao == null)
            throw new ArgumentNullException(nameof(novaMovimentacao));
        
        ValidationHelper.ModelValidation(novaMovimentacao);

        Movimentacao movimentacao = novaMovimentacao.ToMovimentacao();
        
        _db.Add(movimentacao);
        await _db.SaveChangesAsync();
        
        return movimentacao.ToMovimentacaoResponse();
    }

    public async Task<MovimentacaoResponse> AlterarMovimentacao(MovimentacaoUpdateRequest? alteraMovimentacao)
    {
        if(alteraMovimentacao == null)
            throw new ArgumentNullException(nameof(alteraMovimentacao));
        
        Movimentacao? movimentacaoExiste = await _db.Movimentacoes.FirstOrDefaultAsync(temp => temp.Id == alteraMovimentacao.Id);

        if (movimentacaoExiste == null)
            throw new ArgumentException("Movimentação não encontrada");
        
        ValidationHelper.ModelValidation(movimentacaoExiste);
        
        movimentacaoExiste.Nome = alteraMovimentacao.Nome;
        movimentacaoExiste.Valor = alteraMovimentacao.Valor;
        movimentacaoExiste.Descricao =  alteraMovimentacao.Descricao;
        movimentacaoExiste.TipoMovto = alteraMovimentacao.TipoMovto.ToString();
        movimentacaoExiste.DataMovto =  alteraMovimentacao.DataMovto;

        await _db.SaveChangesAsync();
        return movimentacaoExiste.ToMovimentacaoResponse();
    }

    public async Task<bool> ExcluirMovimentacao(Guid? id)
    {
        if(id == null)
            throw new ArgumentNullException(nameof(id));
        
        Movimentacao? movimentacaoEncontrada = await _db.Movimentacoes.FirstOrDefaultAsync(temp => temp.Id == id);
        
        if(movimentacaoEncontrada == null)
            return false;
        
        _db.Remove(_db.Movimentacoes.First(temp => temp.Id == id));
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<MovimentacaoResponse>> ListarMovimentacoes(Guid? usuarioId)
    {
        if (usuarioId == null)
            throw new ArgumentNullException(nameof(usuarioId));
        
        List<Movimentacao> movimentacoesEncontradas = 
            await _db.Movimentacoes.Where(temp => temp.UsuarioId == usuarioId).ToListAsync();
        
        return movimentacoesEncontradas
            .Select(temp => temp.ToMovimentacaoResponse())
            .ToList();
    }

    public async Task<MovimentacaoResponse?> BuscaMovimentacao(Guid? movimentacaoId)
    {
        if(movimentacaoId == null)
            throw new ArgumentNullException(nameof(movimentacaoId));

        Movimentacao? movimentacaoEncontrada =
            await _db.Movimentacoes.FirstOrDefaultAsync(temp => temp.Id == movimentacaoId);

        if (movimentacaoEncontrada == null)
            return null;
        
        return movimentacaoEncontrada.ToMovimentacaoResponse();
    }
}