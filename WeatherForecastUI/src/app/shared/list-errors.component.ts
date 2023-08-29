import { Component, Input } from "@angular/core";

@Component({
  selector: "app-list-errors",
  templateUrl: "./list-errors.component.html"
})
export class ListErrorsComponent {
  errorList: string[] = [];

  @Input() set errors(errorList: string[]) {
    this.errorList = errorList;
  }
}
