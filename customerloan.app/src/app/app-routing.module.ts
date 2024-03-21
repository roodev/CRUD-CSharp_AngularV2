import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersListComponent } from './customers-list/customers-list.component';
import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { UpdateCustomerComponent } from './update-customer/update-customer.component';

const routes: Routes = [
  { path: '', component: CustomersListComponent },
  { path: 'customer-create', component: RegisterCustomerComponent, title: "Cadastrar Cliente" },
  { path: 'update-customer/:id', component: UpdateCustomerComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
