import { LivroDetailService } from './../shared/livro-detail.service';
import { LivroDetail } from './../shared/livro-detail.model';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';


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

  populateForm(selectedRecord: LivroDetail) {
    this.service.formData = Object.assign({}, selectedRecord);


  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deleteLivroDetail(id)
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
