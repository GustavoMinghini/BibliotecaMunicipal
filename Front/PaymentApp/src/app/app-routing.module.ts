import { IndexComponent } from './index/index.component';
import { EmprestimoComponent } from './emprestimo/emprestimo.component';
import { LivrosDetailsComponent } from './livros-details/livros-details.component';
import { LivrosDetailsFromComponent } from './livros-details/livros-details-from/livros-details-from.component';
import { PessoaDetailsComponent } from './pessoa-detail/pessoa-detail.component';
import { PessoaDetail } from 'src/app/shared/pessoa-details.model';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'pessoa', component: PessoaDetailsComponent},
  {path: 'livro', component: LivrosDetailsComponent},
  {path: 'emprestimo', component: EmprestimoComponent},
  {path: '', component: IndexComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
