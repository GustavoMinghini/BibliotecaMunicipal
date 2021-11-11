import { Injectable } from '@angular/core';
import { PessoaDetail } from './pessoa-details.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PessoaDetailService {

  constructor(private http: HttpClient) { }
  

  readonly baseURL = 'https://localhost:44337/api/Pessoas'
  formData: PessoaDetail = new PessoaDetail();
  list: PessoaDetail[];

  postPessoaDetail() {
    return this.http.post(this.baseURL, this.formData);
  }

  putPessoaDetail() {
    let a = this.formData;
    return this.http.put(`${this.baseURL}/${this.formData.pessoaId}`, a);
  }

  deletePessoaDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as PessoaDetail[]);
  }

}
