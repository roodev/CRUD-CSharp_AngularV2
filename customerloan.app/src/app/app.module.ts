import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar'; 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CustomersListComponent } from './customers-list/customers-list.component';
import { RegisterCustomerComponent } from './register-customer/register-customer.component';
import { UpdateCustomerComponent } from './update-customer/update-customer.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomersListComponent,
    RegisterCustomerComponent,
    UpdateCustomerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatSnackBarModule
    

  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync('noop')
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
