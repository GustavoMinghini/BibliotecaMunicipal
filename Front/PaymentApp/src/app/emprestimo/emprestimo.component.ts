import { EmprestimoDetailService } from './../shared/emprestimo-detail.service';
import { EmprestimoDetail } from './../shared/emprestimo-detail.model';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { NgbButtonLabel } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-emprestimo',
  templateUrl: './emprestimo.component.html',
  styleUrls: ['./emprestimo.component.css']
})
export class EmprestimoComponent implements OnInit {

  constructor(public service: EmprestimoDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }
  updateRecord(form: NgForm) {
    this.service.putEmprestimoDetail().subscribe(
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
  onReset(form: NgForm){
    this.resetForm(form);
    this.service.refreshList();
  }



  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new EmprestimoDetail();
  }


  populateForm(selectedRecord: EmprestimoDetail) {
    this.service.formData = Object.assign({}, selectedRecord);

  }




  insertRecord(form: NgForm) {
    this.service.postEmprestimoDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success("Registrado com Sucesso", 'Informações')
      },
      err => { console.log(err);
        this.toastr.error("Erro na Operação", "Verifique os dados");
      }
    );
  }


  onEmprestimo(form: NgForm) {
    if (this.service.formData.requestId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  onDevolver(cpf: string) {
    if (confirm('Você tem certeza que deseja devolver?')) {
      this.service.deleteEmprestimoDetail(cpf)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.success("Realizada com Sucesso", 'Devolução');
          },
          err => { console.log(err)
            this.toastr.error("Erro na Operação", "Verifique os dados");
           }
        )
    }
  }

}























