import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomerloanService } from '../customerloan.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-update-customer',
  templateUrl: './update-customer.component.html',
  styleUrls: ['./update-customer.component.css']
})
export class UpdateCustomerComponent implements OnInit {
  customerForm: FormGroup;
  customerId!: number;
  customerData: any;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private customerService: CustomerloanService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.customerForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(4)]],
      cpf: ['', [Validators.required, Validators.maxLength(11)]]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.customerId = params['id']; 
      this.getCustomerData(); 
    });
  }

  getCustomerData(): void {
    this.customerService.getCustomerById(this.customerId).subscribe(
      (data: any) => {
        this.customerData = data; 
        this.populateForm(); 
      },
      error => {
        console.error('Erro ao obter dados do cliente:', error);
      }
    );
  }

  populateForm(): void {
    this.customerForm.patchValue({
      name: this.customerData.name,
      cpf: this.customerData.cpf
    });
  }

  onSubmit(): void {
    if (this.customerForm.valid) {
      const formData = this.customerForm.value;
      formData.id = this.customerId;
  
      this.customerService.updateCustomer(formData).subscribe(
        response => {
          this.successMessage = 'Cliente cadastrado com sucesso!';
          this.openSnackBar(this.successMessage);
          this.router.navigate(['/']);
        },
        error => {
          this.errorMessage= 'Houve um erro ao atualiozar o Cliente';
          this.openErrorSnackBar(this.errorMessage);
        }
      );
    } else {
      console.log('Formulário inválido. Por favor, verifique os campos.');
    }
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top' 
    });
  }

  openErrorSnackBar(error: any) {
    let errorMessage = 'Não foi possível atualizar o Cliente!';
  
    if (error instanceof HttpErrorResponse) {
      if (error.status === 400 && error.error && error.error.message) {
        errorMessage = error.error.message;
      }
    }
  
    this.snackBar.open(errorMessage, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'start',
      verticalPosition: 'top',
      panelClass: ['error-snackbar']
    });
  }
}
