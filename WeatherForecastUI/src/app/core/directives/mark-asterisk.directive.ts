import { Directive, ElementRef, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: '[required]'
})
export class MarkAsteriskDirective implements OnInit {

  constructor(private readonly renderer: Renderer2, private readonly el: ElementRef) { }

  ngOnInit() {
    const parent = this.renderer.parentNode(this.el.nativeElement);
    if (parent.getElementsByTagName('LABEL').length && !parent.getElementsByClassName('required-asterisk').length) {
      parent.getElementsByTagName('LABEL')[0].innerHTML += '<span class="required-asterisk">*</span>';
    }
  }
}
