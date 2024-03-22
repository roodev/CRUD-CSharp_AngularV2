import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanSimulationComponent } from './loan-simulation.component';

describe('LoanSimulationComponent', () => {
  let component: LoanSimulationComponent;
  let fixture: ComponentFixture<LoanSimulationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoanSimulationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoanSimulationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
