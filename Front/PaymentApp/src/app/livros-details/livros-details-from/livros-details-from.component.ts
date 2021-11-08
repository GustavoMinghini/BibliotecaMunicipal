import { Component, OnInit } from '@angular/core';
import { LivroDetailService } from 'src/app/shared/livro-detail.service';
import { NgForm } from '@angular/forms';
import { LivroDetail } from 'src/app/shared/livro-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-livros-details-from',
  templateUrl: './livros-details-from.component.html',
  styleUrls: ['./livros-details-from.component.css']
})
export class LivrosDetailsFromComponent implements OnInit {

  constructor(public service: LivroDetailService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
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
        this.toastr.success('Submitted successfully', 'Payment Detail Register')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putLivroDetail().subscribe(
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
    this.service.formData = new LivroDetail();
  }

}
