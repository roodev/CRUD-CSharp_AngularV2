import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersListComponent } from './customers-list/customers-list.component';
import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { UpdateCustomerComponent } from './update-customer/update-customer.component';
import { LoanSimulationComponent } from './loan-simulation/loan-simulation.component';

const routes: Routes = [
  { path: '', component: CustomersListComponent },
  { path: 'customer-create', component: RegisterCustomerComponent, title: "Cadastrar Cliente" },
  { path: 'update-customer/:id', component: UpdateCustomerComponent, title: "Atualizar Cliente"},
  { path: 'loan-simulation/:id', component: LoanSimulationComponent, title: "Simulação de Empréstimo"},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
