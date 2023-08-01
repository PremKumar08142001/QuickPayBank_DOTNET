import { TestBed } from '@angular/core/testing';

import { LoanAppliedService } from './loan-applied.service';

describe('LoanAppliedService', () => {
  let service: LoanAppliedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanAppliedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
