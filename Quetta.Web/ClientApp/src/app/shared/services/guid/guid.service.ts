import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';

@Injectable({ providedIn: 'root' })
export class GuidService {
    getValue(): Guid {
        return Guid.create();
    }
}
