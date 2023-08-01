import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectLoanComponent } from './select-loan.component';

describe('SelectLoanComponent', () => {
  let component: SelectLoanComponent;
  let fixture: ComponentFixture<SelectLoanComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectLoanComponent]
    });
    fixture = TestBed.createComponent(SelectLoanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
