import { Component, OnInit } from '@angular/core';
import { PessoaDetailService } from 'src/app/shared/pessoa-details.service';
import { NgForm } from '@angular/forms';
import { PessoaDetail } from 'src/app/shared/pessoa-details.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pessoa-detail',
  templateUrl: './pessoa-detail.component.html',
  styleUrls: ['./pessoa-detail.component.css']
})
export class PessoaDetailsComponent implements OnInit {

  constructor(public service: PessoaDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }
  onSubmit(form: NgForm) {
    if (this.service.formData.pessoaId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }
  onReset(form: NgForm){
    this.resetForm(form);
    this.service.refreshList();
  }

  insertRecord(form: NgForm) {
    this.service.postPessoaDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success("Registrado com Sucesso", 'Informações')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putPessoaDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Atualizado com Sucesso', 'Informações')
      },
      err => { console.log(err);
        this.toastr.error("Erro na Operação", "Verifique os dados");

      }
    );
  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new PessoaDetail();
  }

  populateForm(selectedRecord: PessoaDetail) {
    this.service.formData = Object.assign({}, selectedRecord);


  }

  onDelete(id: number) {
    if (confirm('Você tem certeza que deseja apagar?')) {
      this.service.deletePessoaDetail(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Apagada com Sucesso", 'Informações');
          },
          err => { console.log(err)
            this.toastr.error("Erro na Operação", "Verifique os dados");

          }
        )
    }
  }


}
