import { EmprestimoDetailService } from './../shared/emprestimo-detail.service';
import { EmprestimoDetail } from './../shared/emprestimo-detail.model';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';


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
        this.toastr.info('Updated successfully', 'Payment Detail Register')
      },
      err => { console.log(err); }
    );
  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new EmprestimoDetail();
  }

  populateForm(selectedRecord: EmprestimoDetail) {
    this.service.formData = Object.assign({}, selectedRecord);

  }
  insertRecord(form: NgForm) {
   console.log(form.controls.value);

    this.service.postEmprestimoDetail().subscribe(

      res => {
        this.resetForm(form);
        this.service.refreshList();
        console.log(res);
        this.toastr.success('Submitted successfully', 'Payment Detail Register')
      },
      err => { console.log(err); }
    );
  }
  onEmprestimo(form: NgForm) {
    if (this.service.formData.emprestimoId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deleteEmprestimoDetail(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Deleted successfully", 'Payment Detail Register');
          },
          err => { console.log(err) }
        )
    }
  }

}

