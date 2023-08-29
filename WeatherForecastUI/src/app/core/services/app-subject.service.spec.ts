import { TestBed } from '@angular/core/testing';

import { AppSubjectService } from './app-subject.service';

describe('AppSubjectService', () => {
  let service: AppSubjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppSubjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
