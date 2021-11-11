import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';
import { PaymentDetailFormComponent } from './payment-details/payment-detail-form/payment-detail-form.component';
import { HttpClientModule } from '@angular/common/http';
import { LivrosDetailsComponent } from './livros-details/livros-details.component';
import { LivrosDetailsFromComponent } from './livros-details/livros-details-from/livros-details-from.component';
import { PessoaDetailsComponent } from './pessoa-detail/pessoa-detail.component';
import { EmprestimoComponent } from './emprestimo/emprestimo.component';
import { AppRoutingModule } from './app-routing.module';
import { IndexComponent } from './index/index.component';




@NgModule({
  declarations: [
    AppComponent,
    PaymentDetailsComponent,
    PaymentDetailFormComponent,
    LivrosDetailsComponent,
    LivrosDetailsFromComponent,
    PessoaDetailsComponent,
    EmprestimoComponent,
    IndexComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
