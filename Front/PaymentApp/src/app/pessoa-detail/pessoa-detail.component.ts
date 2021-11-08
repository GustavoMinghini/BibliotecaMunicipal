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

  insertRecord(form: NgForm) {
    this.service.postPessoaDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Submitted successfully', 'Payment Detail Register')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putPessoaDetail().subscribe(
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
    this.service.formData = new PessoaDetail();
  }

  populateForm(selectedRecord: PessoaDetail) {
    this.service.formData = Object.assign({}, selectedRecord);


  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deletePessoaDetail(id)
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