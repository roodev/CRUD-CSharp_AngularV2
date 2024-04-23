import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerloanService } from '../customerloan.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
    private customerLoanService: CustomerloanService,
    private snackBar: MatSnackBar
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
    this.getCurrencies();

  }

  onDateChange(event: any) {
    const loanAmount = this.loanForm.get('loanAmount')?.value;
    const currency = this.loanForm.get('currency')?.value;
    const selectedDate = event.format('MM-DD-YYYY');
    const currencyType = currency.tipoMoeda;
    const currencySymbol = currency.simbolo;
    const customerId = this.customerId;
    const dueDate = selectedDate;

    this.customerLoanService.sendLoanData(currencyType, currencySymbol, loanAmount, selectedDate, customerId).subscribe(
      (response) => {
        this.loanForm.patchValue({
          monthsToDueDate: response.monthsToDueDate,
          totalAmount: response.totalAmount ? response.totalAmount : ''
        });
      },
      (error) => {
        console.error('Erro ao enviar dados para o backend:', error);
      }
    );
  }

  onCurrencySelectionChange(event: any) {
    const selectedCurrency = event.value;
    const selectedCurrencyType = selectedCurrency.tipoMoeda;
    const selectedCurrencySymbol = selectedCurrency.simbolo;
    const selectedCustomerId = this.customerId;
  }

  submitLoan(): void {
    if (this.loanForm.valid) {
      const loanData = {
        Currency: this.loanForm.get('currency')?.value.simbolo,
        Amount: this.loanForm.get('loanAmount')?.value,
        DueDate: this.loanForm.get('dueDate')?.value,
        TotalAmount: this.loanForm.get('totalAmount')?.value,
        MonthsToDueDate: this.loanForm.get('monthsToDueDate')?.value,
        CustomerId: this.customerId
      };

      this.customerLoanService.createLoan(loanData).subscribe(
        (response) => {
          console.log('Empréstimo criado com sucesso!', response);
          this.openSnackBar('Empréstimo criado com sucesso!');
        },
        (error) => {
          console.error('Erro ao criar empréstimo:', error);
          this.openErrorSnackBar('Erro ao criar empréstimo');
        }
      );
    } else {
      console.error('Formulário inválido, não foi possível criar o empréstimo.');
      this.openErrorSnackBar('Formulário inválido');
    }
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top'
    });
  }

  openErrorSnackBar(errorMessage: string) {
    this.snackBar.open(errorMessage, 'Fechar', {
      duration: 3000,
      horizontalPosition: 'start',
      verticalPosition: 'top',
      panelClass: ['error-snackbar']
    });
  }


  resetForm(): void {
    this.loanForm.reset();
  }

  getCurrencies(): void {
    this.customerLoanService.getCurrencies().subscribe(
      (response) => {
        this.currencies = response;
        this.currencies.unshift({ simbolo: 'BRL', nomeFormatado: 'Real Brasileiro', tipoMoeda: 'BRL' });
      },
      (error) => {
        console.error('Erro ao obter as moedas:', error);
      }
    );
  }

  getFormattedTotalAmount(): string {
    const totalAmountControl = this.loanForm.get('totalAmount');
    if (totalAmountControl) {
      const totalAmountValue = totalAmountControl.value;
      if (totalAmountValue != null && totalAmountValue !== '') {
        return totalAmountValue;
      }
    }
    return '';
  }
}
