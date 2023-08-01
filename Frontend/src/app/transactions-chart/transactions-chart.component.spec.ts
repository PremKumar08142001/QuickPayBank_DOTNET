import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionsChartComponent } from './transactions-chart.component';

describe('TransactionsChartComponent', () => {
  let component: TransactionsChartComponent;
  let fixture: ComponentFixture<TransactionsChartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TransactionsChartComponent]
    });
    fixture = TestBed.createComponent(TransactionsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
