import { TestBed } from '@angular/core/testing';

import { CustomerloanService } from './customerloan.service';

describe('CustomerloanService', () => {
  let service: CustomerloanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerloanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
