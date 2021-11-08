import { Injectable } from '@angular/core';
import { LivroDetail } from './livro-detail.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LivroDetailService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'https://localhost:44337/api/Livros'
  formData: LivroDetail = new LivroDetail();
  list: LivroDetail[];

  postLivroDetail() {
    return this.http.post(this.baseURL, this.formData);
  }

  putLivroDetail() {
    let a = this.formData;
    return this.http.put(`${this.baseURL}/${this.formData.livroId}`, a);
  }

  deleteLivroDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as LivroDetail[]);
  }

}
