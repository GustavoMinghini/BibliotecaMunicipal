import { Injectable } from '@angular/core';
import { EmprestimoDetail } from './emprestimo-detail.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EmprestimoDetailService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'https://localhost:44337/api/Emprestimos'
  readonly urlDelete = 'https://localhost:44337/Devolver?cpf='
  formData: EmprestimoDetail = new EmprestimoDetail();
  list: EmprestimoDetail[];

  postEmprestimoDetail() {
    return this.http.post(this.baseURL,this.formData);
  }

  putEmprestimoDetail() {
    let a = this.formData;
    return this.http.put(`${this.baseURL}/${this.formData.requestId}`, a);
  }

  deleteEmprestimoDetail(cpf: string) {
    return this.http.put(`${this.urlDelete}${cpf}`, 123123);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as EmprestimoDetail[]);
  }

}
