﻿using Crosscuting.Notificacao;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class AlunoRepositorio : BaseRepositorio<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(Context context, INotificador notificador) : base(context, notificador)
        {
        }

        public async override Task<IQueryable<Aluno>> GetAsync(Func<Aluno, bool> filter = null)
        {
            await Task.Yield();
            return filter is null ?
               Context.Aluno.Include(x => x.Curso).Include(x => x.AlunoDisciplina).ThenInclude(x => x.Disciplina).AsQueryable() :
                Context.Aluno.Include(x => x.Curso).Include(x => x.AlunoDisciplina).ThenInclude(x => x.Disciplina).Where(filter).AsQueryable();
        }
    }
}
