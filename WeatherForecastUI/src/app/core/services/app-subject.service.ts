import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppSubjectService {
  subject = new Subject();

  constructor() { }

  subscribe(observablaData: Observable<any> | null) {
    this.subject.next(observablaData);
  }

  getSubjectAsObservable(): Observable<any> {
    return this.subject.asObservable();
  }

  unSubscribe() {
    this.subject.unsubscribe();
  }
}
