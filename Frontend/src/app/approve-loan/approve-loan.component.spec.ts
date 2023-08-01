import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveLoanComponent } from './approve-loan.component';

describe('ApproveLoanComponent', () => {
  let component: ApproveLoanComponent;
  let fixture: ComponentFixture<ApproveLoanComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApproveLoanComponent]
    });
    fixture = TestBed.createComponent(ApproveLoanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
