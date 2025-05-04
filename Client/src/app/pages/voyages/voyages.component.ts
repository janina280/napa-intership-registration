import {Component, OnInit} from '@angular/core';
import {Voyage} from '../../models/voyage';
import {VoyageService} from '../../services/voyage.service';

@Component({
  selector: 'app-voyages',
  imports: [],
  templateUrl: './voyages.component.html',
  styleUrl: './voyages.component.css'
})
export class VoyagesComponent implements OnInit{
  voyages: Voyage[] = [];

  constructor(private voyageService: VoyageService) {}

  ngOnInit(): void {
    this.voyageService.getAll().subscribe(data => {
      this.voyages = data;
    });
  }
}
