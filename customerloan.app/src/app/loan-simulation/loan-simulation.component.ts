import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerloanService } from '../customerloan.service';

@Component({
  selector: 'app-loan-simulation',
  templateUrl: './loan-simulation.component.html',
  styleUrls: ['./loan-simulation.component.css']
})
export class LoanSimulationComponent implements OnInit {
  loanForm: FormGroup;
  customerId!: number;
  currencies: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private customerService: CustomerloanService
  ) {
    this.loanForm = this.formBuilder.group({
      loanAmount: ['', Validators.required],
      currency: ['', Validators.required],
      dueDate: ['', Validators.required],
      monthsToDueDate: [''],
      totalAmount: ['']
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.customerId = params['id']; 
    });

  }

  submitLoan(): void {
  }

  resetForm(): void {
    this.loanForm.reset();
  }

  simulateLoan(): void {
    console.log('Simulando empréstimo...');
  }

  getCurrencies(): void {
    this.customerService.getCurrencies().subscribe(
      (response) => {
        this.currencies = response;
      },
      (error) => {
        console.error('Erro ao obter as moedas:', error);
      }
    );
  }
}
