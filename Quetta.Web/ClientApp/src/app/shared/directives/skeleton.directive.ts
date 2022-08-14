import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
    // eslint-disable-next-line @angular-eslint/directive-selector
    selector: '[skeletonFor]',
})
export class SkeletonDirective implements OnChanges {
    @Input() skeletonFor: unknown;
    @Input() skeletonClass!: string;

    constructor(private element: ElementRef) {}

    ngOnChanges(): void {
        if (this.skeletonFor) {
            this.element.nativeElement.classList.remove(this.skeletonClass);
        } else {
            this.element.nativeElement.classList.add(this.skeletonClass);
        }
    }
}
