import { LivroDetailService } from './../shared/livro-detail.service';
import { LivroDetail } from './../shared/livro-detail.model';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';



@Component({
  selector: 'app-livros-details',
  templateUrl: './livros-details.component.html',
  styleUrls: ['./livros-details.component.css']
})
export class LivrosDetailsComponent implements OnInit {

  constructor(public service: LivroDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }
  updateRecord(form: NgForm) {
    this.service.putLivroDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Com Sucesso', 'Atualizado')
      },
      err => { console.log(err);
        this.toastr.error("Erro na Operação", "Verifique os dados");

      }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new LivroDetail();
  }
  onReset(form: NgForm){
    this.resetForm(form);
    this.service.refreshList();
  }



  onSubmit(form: NgForm) {
    if (this.service.formData.livroId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }
  insertRecord(form: NgForm) {
    this.service.postLivroDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Registrada com Sucesso', 'Informações')
      },
      err => { console.log(err);
        this.toastr.error("Erro na Operação", "Verifique os dados");

      }
    );
  }

  populateForm(selectedRecord: LivroDetail) {
    this.service.formData = Object.assign({}, selectedRecord);


  }

  onDelete(id: number) {
    if (confirm('Você tem certeza que deseja apagar?')) {
      this.service.deleteLivroDetail(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Deletado com Sucesso", 'Livro');
          },
          err => { console.log(err)
            this.toastr.error("Erro na Operação", "Verifique os dados");
          }
        )
    }
  }

}
