import { Component, OnInit } from '@angular/core';
import { CustomerloanService } from '../customerloan.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css']
})
export class CustomersListComponent implements OnInit {

  customers: any[] = [];
  editingCustomer: any = null; // Variável para armazenar o cliente em edição

  // Defina a propriedade displayedColumns como um array de strings
  displayedColumns: string[] = ['name', 'cpf', 'actions'];

  constructor(private customerService: CustomerloanService, private router: Router) { }

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers(): void {
    this.customerService.getCustomers()
      .subscribe(data => {
        this.customers = data;
      });
  }

  deleteCustomer(id: number): void {
    this.customerService.deleteCustomer(id).subscribe(
      () => {
        console.log('Cliente excluído com sucesso.');
        this.getCustomers();
      },
      error => {
        console.error('Erro ao excluir cliente:', error);
      }
    );
  }

  simulateLoan(customerId: number): void {
    this.router.navigate(['/customer-loan-simulation', customerId]);
  }
}
