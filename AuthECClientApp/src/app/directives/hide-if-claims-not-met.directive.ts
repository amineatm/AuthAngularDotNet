import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { BrowserService } from '../shared/services/browser.service';

@Directive({
  selector: '[appHideIfClaimsNotMet]',
  standalone: true
})
export class HideIfClaimsNotMetDirective implements OnInit {
  @Input("appHideIfClaimsNotMet") claimReq!: Function;

  constructor(private browserService: BrowserService,
    private elementRef: ElementRef) { }

  ngOnInit(): void {
    const claims = this.browserService.getClaims();
    
    if (!this.claimReq(claims))
      this.elementRef.nativeElement.style.display = "none";
  }

} 