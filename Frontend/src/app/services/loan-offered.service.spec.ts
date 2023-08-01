import { TestBed } from '@angular/core/testing';

import { LoanOfferedService } from './loan-offered.service';

describe('LoanOfferedService', () => {
  let service: LoanOfferedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanOfferedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
