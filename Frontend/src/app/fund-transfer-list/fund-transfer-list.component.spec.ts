import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FundTransferListComponent } from './fund-transfer-list.component';

describe('FundTransferListComponent', () => {
  let component: FundTransferListComponent;
  let fixture: ComponentFixture<FundTransferListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FundTransferListComponent]
    });
    fixture = TestBed.createComponent(FundTransferListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
