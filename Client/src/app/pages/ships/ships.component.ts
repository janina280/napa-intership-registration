import {Component, OnInit} from '@angular/core';
import {Ship} from '../../models/ship';
import {ShipService} from '../../services/ship.service';

@Component({
  selector: 'app-ships',
  imports: [],
  templateUrl: './ships.component.html',
  styleUrl: './ships.component.css'
})
export class ShipsComponent implements OnInit {
  ships: Ship[] = [];

  constructor(private shipService: ShipService) {
  }

  ngOnInit(): void {
    this.shipService.getAll().subscribe(data => {
      this.ships = data;
    });
  }
}
