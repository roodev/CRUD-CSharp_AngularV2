import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerloanService } from '../customerloan.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-costumer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.css']
})
export class RegisterCustomerComponent {
  customerForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerloanService,
    private snackBar: MatSnackBar,
    private router: Router,
  ) {
    this.customerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(4)]],
      cpf: ['', [Validators.required, Validators.maxLength(11), this.validateCpf.bind(this)]]
    });
  }

  onSubmit() {
    if (this.customerForm.valid) {
      const formData = this.customerForm.value;
      this.customerService.createCustomer(formData).subscribe(
        response => {
          this.successMessage = 'Cliente cadastrado com sucesso!';
          this.openSnackBar(this.successMessage);
          this.router.navigate(['/']);
        },
        error => {
          this.errorMessage = 'Houve um erro ao processo o cadastro de Cliente.';
          this.openErrorSnackBar(this.errorMessage);
        }
      );

      this.customerForm.reset();
    } else {
      console.log('Formulário inválido. Por favor, verifique os campos.');
    }
  }

  validateCpf(control: any) {
    if (control && control.value) {
      let cpf = control.value.replace(/\D/g, '');


      if (cpf.length > 11) {
        cpf = cpf.substr(0, 11);
        control.setValue(cpf);
      }


      if (cpf.length !== 11) {
        return { invalidCpf: true };
      }
    }

    return null;
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top'
    });
  }

  openErrorSnackBar(error: any) {
    let errorMessage = 'CPF já cadastrado!';

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
